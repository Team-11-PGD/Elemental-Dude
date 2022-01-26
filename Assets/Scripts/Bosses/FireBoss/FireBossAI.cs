using UnityEngine;

[RequireComponent(typeof(BossMoveToPlayerState), typeof(BossLavaSlamAttack), typeof(BossFlameBreathAttack))]
[RequireComponent(typeof(BossOrbitalBeamAttack), typeof(BossLavaStreamAttack), typeof(BossDeath))]
public class FireBossAI : BossAI
{
    [SerializeField] 
    StateOptions startState;
    [SerializeField]
    [Range(0, 1)]
    float nextPercentageStep = 0.33f;
    [SerializeField]
    [Range(0, 1)]
    float nextPhasePercentage = 0.66f;
    [SerializeField] int healthPickupAmount = 3;
    [SerializeField] GameObject fallingHealthPickup;
    [SerializeField] BoxCollider healthSpawnArea;
    [SerializeField] GameObject uiObjectHealthPickup;

    BossLavaSlamAttack slamAttack;
    BossFlameBreathAttack flameBreathAttack;
    BossOrbitalBeamAttack orbitalBeamState;
    BossLavaStreamAttack lavaStreamState;

    public enum StateOptions
    {
        MoveToPlayer,
        FireAttacking1,
        FireAttacking2,
        Defending1,
        Defendig2,
        Death
    }


    void Start()
    {
        slamAttack = GetComponent<BossLavaSlamAttack>();
        flameBreathAttack = GetComponent<BossFlameBreathAttack>();
        orbitalBeamState = GetComponent<BossOrbitalBeamAttack>();
        lavaStreamState = GetComponent<BossLavaStreamAttack>();

        AddState(StateOptions.MoveToPlayer, GetComponent<BossMoveToPlayerState>());          // 0
        AddState(StateOptions.FireAttacking1, slamAttack);                                   // 1
        AddState(StateOptions.FireAttacking2, flameBreathAttack);                            // 2
        AddState(StateOptions.Defending1, orbitalBeamState);                                 // 3
        AddState(StateOptions.Defendig2, lavaStreamState);                                   // 4
        AddState(StateOptions.Death, GetComponent<BossDeath>());                             // 5

        StateMachineSetup(startState);

        transform.localScale *= 3.5f;
    }

    public override void NextState()
    {
        switch ((StateOptions)CurrentStateId)
        {
            case StateOptions.MoveToPlayer:
                NextRandomState(true, StateOptions.Death);
                break;
            default:
                if (health.HpPercentage < nextPhasePercentage) NextPhase();
                TransitionTo(StateOptions.MoveToPlayer);
                break;
        }
    }

    protected override void Died()
    {
        TransitionTo(StateOptions.Death);
        enabled = false;
    }

    void NextPhase()
    {
        nextPhasePercentage -= nextPercentageStep;

        // Increase power
        slamAttack.lavaSize *= 1.5f;
        flameBreathAttack.size *= 1.5f;
        lavaStreamState.attackAmount++;
        orbitalBeamState.orbitalBeamAmount += 3;

        // Spawn health
        for (int i = 0; i < healthPickupAmount; i++)
        {
            Vector3 spawningPosition = new Vector3(
                Random.Range(healthSpawnArea.bounds.min.x, healthSpawnArea.bounds.max.x),
                Random.Range(healthSpawnArea.bounds.min.y, healthSpawnArea.bounds.max.y),
                Random.Range(healthSpawnArea.bounds.min.z, healthSpawnArea.bounds.max.z));

            spawningPosition = healthSpawnArea.ClosestPoint(spawningPosition);

            GameObject instance = Instantiate(fallingHealthPickup, spawningPosition, Quaternion.identity, null);
            instance.GetComponent<ShowPickupText>().uiObject = uiObjectHealthPickup;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossMoveToPlayerState), typeof(BossLavaSlamAttack), typeof(BossFlameBreathAttack))]
[RequireComponent(typeof(BossDefendingFireBallState), typeof(BossDefendingLavaStreamState), typeof(BossMoveToPosition))]
[RequireComponent(typeof(BossDeath))]
public class FireBossAI : BossAI
{
    [SerializeField]
    Health shieldHealth;
    [SerializeField]
    GameObject shield;
    [SerializeField]
    [Range(0, 1)]
    float nextPercentageStep = 0.33f;
    [SerializeField]
    [Range(0, 1)]
    float nextStatePercentage = 0.66f;
    [SerializeField]
    int healthPickupAmount = 3;
    [SerializeField]
    GameObject fallingHealthPickup;
    [SerializeField]
    BoxCollider healthSpawnArea;
    [SerializeField]
    GameObject uiObjectHealthPickup;

    int currentStage = 1;
    BossLavaSlamAttack slamAttack;
    BossFlameBreathAttack flameBreathAttack;
    BossDefendingFireBallState fireBallState;
    BossDefendingLavaStreamState lavaStreamState;
    List<GameObject> healthPickups;

    public enum StateOptions
    {
        MoveToPlayer,
        FireAttacking1,
        FireAttacking2,
        MoveToCenter,
        Defending1,
        Defendig2,
        Death
    }

    [SerializeField]
    protected StateOptions startState;

    protected void Start()
    {
        slamAttack = GetComponent<BossLavaSlamAttack>();
        flameBreathAttack = GetComponent<BossFlameBreathAttack>();
        fireBallState = GetComponent<BossDefendingFireBallState>();
        lavaStreamState = GetComponent<BossDefendingLavaStreamState>();

        AddState(StateOptions.MoveToPlayer, GetComponent<BossMoveToPlayerState>());          // 0
        AddState(StateOptions.FireAttacking1, slamAttack);                                   // 1
        AddState(StateOptions.FireAttacking2, flameBreathAttack);                            // 2
        AddState(StateOptions.MoveToCenter, GetComponent<BossMoveToPosition>());             // 3
        AddState(StateOptions.Defending1, fireBallState);                                    // 4
        AddState(StateOptions.Defendig2, lavaStreamState);                                   // 5
        AddState(StateOptions.Death, GetComponent<BossDeath>());                             // 6

        StateMachineSetup(startState);
    }

    public override void NextState()
    {
        switch (CurrentStateId)
        {
            case (int)StateOptions.FireAttacking1:
            case (int)StateOptions.FireAttacking2:
                if (!SwitchToDefend()) TransitionTo(StateOptions.MoveToPlayer);
                break;
            case (int)StateOptions.MoveToPlayer:
                NextAttackState();
                break;
            case (int)StateOptions.Defending1:
            case (int)StateOptions.Defendig2:
            case (int)StateOptions.MoveToCenter:
                health.enabled = true;
                shieldHealth.enabled = true;
                NextDefendState();
                break;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        shieldHealth.Died += ShieldDied;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        shieldHealth.Died -= ShieldDied;
    }

    protected override void Hitted()
    {
        SwitchToDefend();
    }

    protected override void Died()
    {
        if (currentStage == 4)
        {     
            TransitionTo(StateOptions.Death);
            //SOUND: (boss death sound)
        }
    }

    void ShieldDied()
    {
        TransitionTo(StateOptions.MoveToPlayer);
        //SOUND: (Shield destroyed)
        shield.SetActive(false);
        for (int i = 0; i < healthPickups.Count; i++)
        {
            if (healthPickups[i] != null) Destroy(healthPickups[i]);
        }
        NextStage();
    }

    void NextStage()
    {
        currentStage++;
        slamAttack.lavaSize *= 1.5f;
        flameBreathAttack.size*= 1.5f;
        lavaStreamState.instantiateAmount++;
        fireBallState.percentageOfRoomFilled += 0.2f;
    }

    public void NextAttackState()
    {
        if (!SwitchToDefend()) TransitionTo(RandomStateFromRange(StateOptions.FireAttacking1, StateOptions.FireAttacking2));
    }

    public bool SwitchToDefend()
    {
        if (health.HpPercentage <= nextStatePercentage && currentStage < 4)
        {
            nextStatePercentage -= nextPercentageStep;
            TransitionTo(StateOptions.MoveToCenter);
            health.enabled = false;
            shield.SetActive(true);
            shieldHealth.currentHp = shieldHealth.maxHp;
            shieldHealth.enabled = false;

            // Health spawning
            healthPickups = new List<GameObject>();
            for (int i = 0; i < healthPickupAmount; i++)
            {
                Vector3 spawningPosition = new Vector3(
                Random.Range(healthSpawnArea.bounds.min.x, healthSpawnArea.bounds.max.x),
                Random.Range(healthSpawnArea.bounds.min.y, healthSpawnArea.bounds.max.y),
                Random.Range(healthSpawnArea.bounds.min.z, healthSpawnArea.bounds.max.z));

                healthPickups.Add(Instantiate(fallingHealthPickup, spawningPosition, Quaternion.identity));
                healthPickups[healthPickups.Count - 1].GetComponent<ShowPickupText>().uiObject = uiObjectHealthPickup;
            }
            return true;
        }
        return false;
    }

    public void NextDefendState()
    {
        if (shieldHealth.HpPercentage > 0) TransitionTo(RandomStateFromRange(StateOptions.Defending1, StateOptions.Defendig2));
        else
        {
            TransitionTo(StateOptions.MoveToPlayer);
        }
    }

    private StateOptions RandomStateFromRange(StateOptions minInclusive, StateOptions maxInclusive)
        => (StateOptions)Random.Range((int)minInclusive, (int)maxInclusive + 1);


    private void FixedUpdate()
    {
        if (CurrentStateId == 3)
        {
            shield.tag = "UnhittableShield";
        }
        else shield.tag = "BossShield";
    }


}
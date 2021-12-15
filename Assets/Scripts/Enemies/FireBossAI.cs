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
        AddState(StateOptions.MoveToPlayer, GetComponent<BossMoveToPlayerState>());          // 0
        AddState(StateOptions.FireAttacking1, GetComponent<BossLavaSlamAttack>());           // 1
        AddState(StateOptions.FireAttacking2, GetComponent<BossFlameBreathAttack>());        // 2
        AddState(StateOptions.MoveToCenter, GetComponent<BossMoveToPosition>());             // 3
        AddState(StateOptions.Defending1, GetComponent<BossDefendingFireBallState>());       // 4
        AddState(StateOptions.Defendig2, GetComponent<BossDefendingLavaStreamState>());      // 5
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
        TransitionTo(StateOptions.Death);
        //SOUND: (boss death sound)
    }

    void ShieldDied()
    {
        TransitionTo(StateOptions.MoveToPlayer);
        //SOUND: (Shield destroyed)
    }

    public void NextAttackState()
    {
        if (!SwitchToDefend()) TransitionTo(RandomStateFromRange(StateOptions.FireAttacking1, StateOptions.FireAttacking2));
    }

    public bool SwitchToDefend()
    {
        if (health.HpPercentage <= nextStatePercentage)
        {
            nextStatePercentage -= nextPercentageStep;
            TransitionTo(StateOptions.MoveToCenter);
            health.enabled = false;
            shield.SetActive(true);
            shieldHealth.currentHp = shieldHealth.maxHp;
            shieldHealth.enabled = false;
            return true;
        }
        shield.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossMoveToPlayerState), typeof(BossLavaSlamAttack), typeof(BossFlameBreathAttack))]
[RequireComponent(typeof(BossDefendingFireBallState), typeof(BossDefendingLavaStreamState), typeof(BossMoveToPosition))]
public class FireBossAI : BossAI
{
    Health shieldHealth;

    [SerializeField]
    int nextPercentageStep = 33;
    [SerializeField]
    float nextStatePercentage;

    public enum StateOptions
    {
        MoveToPlayer,
        FireAttacking1,
        FireAttacking2,
        MoveToCenter,
        Defending1,
        Defendig2
    }

    [SerializeField]
    protected StateOptions startState;

    protected void Start()
    {
        states.Add((int)StateOptions.MoveToPlayer, GetComponent<BossMoveToPlayerState>());          // 0
        states.Add((int)StateOptions.FireAttacking1, GetComponent<BossLavaSlamAttack>());           // 1
        states.Add((int)StateOptions.FireAttacking2, GetComponent<BossFlameBreathAttack>());        // 2
        states.Add((int)StateOptions.MoveToCenter, GetComponent<BossMoveToPosition>());             // 3
        states.Add((int)StateOptions.Defending1, GetComponent<BossDefendingFireBallState>());       // 4
        states.Add((int)StateOptions.Defendig2, GetComponent<BossDefendingLavaStreamState>());      // 5

        StateMachineSetup((int)startState);
    }

    public override void NextState()
    {
        switch (CurrentStateId)
        {
            case (int)StateOptions.FireAttacking1:
            case (int)StateOptions.FireAttacking2:
                if (!SwitchToDefend()) TransitionTo((int)StateOptions.MoveToPlayer);
                break;
            case (int)StateOptions.MoveToPlayer:
                NextAttackState();
                break;
            case (int)StateOptions.Defending1:
            case (int)StateOptions.Defendig2:
            case (int)StateOptions.MoveToCenter:
                NextDefendState();
                break;
        }
    }

    public void NextAttackState()
    {
        if (!SwitchToDefend()) TransitionTo(Random.Range(1, 3));
    }

    public bool SwitchToDefend()
    {
        if (health.HpPercentage < nextStatePercentage)
        {
            nextStatePercentage -= nextPercentageStep;
            TransitionTo((int)StateOptions.MoveToCenter);
            return true;
        }
        return false;
    }

    public void NextDefendState()
    {
        if (shieldHealth.HpPercentage > 0) TransitionTo(Random.Range(4, 6));
        else TransitionTo((int)StateOptions.MoveToPlayer);
    }


}

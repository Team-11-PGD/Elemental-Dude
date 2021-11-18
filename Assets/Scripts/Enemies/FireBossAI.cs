using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossMoveToPlayerState), typeof(BossLavaSlamAttack), typeof(BossFlameBreathAttack))]
[RequireComponent(typeof(BossDefendingFireBallState), typeof(BossDefendingLavaStreamState))]
public class FireBossAI : BossAI
{
    [SerializeField]
    Transform roomCenter;

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
        states.Add((int)StateOptions.MoveToCenter, GetComponent<BossFlameBreathAttack>());        // 3
        states.Add((int)StateOptions.Defending1, GetComponent<BossDefendingFireBallState>());       // 4
        states.Add((int)StateOptions.Defendig2, GetComponent<BossDefendingLavaStreamState>());      // 5

        StateMachineSetup((int)startState);
    }

    public void NextState()
    {
        if (health.HpPercentage < nextStatePercentage)
        {
            TransitionTo(Random.Range(3, 5));
        }
        else
        {
            if (CurrentStateId == (int)StateOptions.MoveToPlayer)
            {
                TransitionTo(Random.Range(1, 3));
            }
            else
            {
                TransitionTo(0);
            }
        }
    }
}

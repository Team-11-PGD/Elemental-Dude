using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class EnemyHealState : EnemyState
{
    [SerializeField]
    Health enemyHealth;
    [SerializeField]
    [Range(0, 1)]
    float neededHealthPercentage = 0.7f;
    [SerializeField]
    float healTime = 1f;
    [SerializeField]
    int healAmount = 1;

    public override void Enter()
    {
        InvokeRepeating(nameof(Heal), 0, healTime);
    }

    public override void Exit()
    {
        CancelInvoke(nameof(Heal));
    }

    void Heal()
    {
        enemyHealth.Heal(healAmount);
        if (enemyHealth.HpPercentage >= neededHealthPercentage)
        {
            context.TransitionTo((int)EnemyAI.StateOptions.MoveToPlayer);
        }
    }
}


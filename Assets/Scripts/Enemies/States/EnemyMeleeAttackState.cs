using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAttackState : EnemyState
{
    [SerializeField]
    float meleeDistance = 2;
    [SerializeField]
    float attackChargeTime = 1.5f;
    [SerializeField]
    float damage = 1f;

    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Attack());
    }

    public override void Exit() { }

    IEnumerator Attack()
    {
        // Play charge animation
        Debug.Log("start attack animation");

        yield return new WaitForSecondsRealtime(attackChargeTime);
        if (Vector3.Distance(enemyAI.playerModel.position, transform.position) <= meleeDistance)
        {
            enemyAI.playerHealth.Hit(damage);
            StartCoroutine(Attack());
        }
        else
        {
            context.TransitionTo((int)EnemyAI.StateOptions.MoveToPlayer);
        }
    }
}

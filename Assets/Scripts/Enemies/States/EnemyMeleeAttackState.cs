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

    public override void Enter(int previousStateId)
    {
        base.Enter(previousStateId);
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        // Play charge animation
        // SOUND: (Atack)
        Debug.Log("start attack animation");

        if (Vector3.Distance(enemyAI.playerModel.position, transform.position) <= meleeDistance)
        {
            enemyAI.playerHealth.Hit(damage);
            yield return new WaitForSecondsRealtime(attackChargeTime);
            StartCoroutine(Attack());
        }
        else
        {
            context.TransitionTo(EnemyAI.StateOptions.MoveToPlayer);
        }
    }
}

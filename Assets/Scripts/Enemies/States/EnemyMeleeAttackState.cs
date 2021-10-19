using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAttackState : State
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Health playerHealth;
    [SerializeField]
    float meleeDistance = 2;
    [SerializeField]
    float attackChargeTime = 1.5f;
    [SerializeField]
    float damage = 1f;

    public override void Enter()
    {
        StartCoroutine(Attack());
    }

    public override void Exit() { }

    IEnumerator Attack()
    {
        // Play charge animation
        Debug.Log("start attack animation");

        yield return new WaitForSecondsRealtime(attackChargeTime);
        if (Vector3.Distance(player.position, transform.position) <= meleeDistance)
        {
            playerHealth.Hit(damage);
            StartCoroutine(Attack());
        }
        else
        {
            context.TransitionTo((int)EnemyAI.StateOptions.MoveToPlayer);
        }
    }
}

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
    NavMeshAgent navMeshAgent;
    [SerializeField]
    float meleeDistance = 2;
    [SerializeField]
    float attackChargeTime = 1.5f;
    [SerializeField]
    float damage = 1f;

    public override void Enter()
    {
        StartCoroutine(AttackTimer());
    }

    public override void Exit() { }

    IEnumerator AttackTimer()
    {
        // Play charge animation
        navMeshAgent.Warp(transform.position + Vector3.up * 5);// Tmp feedback

        yield return new WaitForSecondsRealtime(attackChargeTime);
        if (Vector3.Distance(player.position, transform.position) <= meleeDistance)
        {
            playerHealth.Hit(damage);
        }
    }
}

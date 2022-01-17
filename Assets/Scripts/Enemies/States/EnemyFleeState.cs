using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class EnemyFleeState : EnemyState
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float fleeRange = 30;

    bool cornerCheck = false;

    public override void Enter(int previousStateId)
    {
        base.Enter(previousStateId);
        NewFleeDestination();
        StartCoroutine(CornerTimer());
    }

    public override void Exit(int nextStateId) { }

    void NewFleeDestination()
    {
        Vector3 dirToPlayer = enemyAI.playerModel.position - transform.position;
        Vector3 fleePos = transform.position - dirToPlayer;
        agent.SetDestination(fleePos);
    }

    void Update()
    {
        

        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            NewFleeDestination();
        }

        if (Vector3.Distance(enemyAI.playerModel.position, transform.position) >= fleeRange || (agent.velocity == Vector3.zero && cornerCheck))
        {
            context.TransitionTo(EnemyAI.StateOptions.Heal);
        }
    }

    IEnumerator CornerTimer()
    {
        yield return new WaitForSecondsRealtime(1);
        cornerCheck = true;
    }
}


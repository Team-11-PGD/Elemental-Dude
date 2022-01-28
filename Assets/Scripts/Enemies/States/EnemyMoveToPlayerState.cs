using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveToPlayerState : EnemyState
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float stopRange = 1.5f;

    public override void Enter(int previousStateId)
    {
        base.Enter(previousStateId);
        Funnel.Instance.funnelEvents.Add(new Funnel.FunnelEvent
        {
            name = "EnemyMoveToPlayer",
            data = new Dictionary<string, object>()
            {
                { "Distance", Vector3.Distance(transform.position, enemyAI.playerModel.transform.position) }
            }
        });
    }

    public override void Exit(int nextStateId) { }

    void Update()
    {
        agent.SetDestination(enemyAI.playerModel.position);
        if (Vector3.Distance(enemyAI.playerModel.position, transform.position) <= stopRange)
        {
            agent.SetDestination(transform.position);
            context.TransitionTo(EnemyAI.StateOptions.Attacking);
        }
    }

}

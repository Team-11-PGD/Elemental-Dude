using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayerState : State
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float stopRange = 1.5f;
    [SerializeField]
    Transform player;

    public override void Enter() { }

    public override void Exit() { }

    void Update()
    {
        agent.SetDestination(player.position);
        if (Vector3.Distance(player.position, transform.position) <= stopRange)
        {
            agent.SetDestination(transform.position);
            context.TransitionTo((int)BossAI.StateOptions.Attacking);
        }
    }

}

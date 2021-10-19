using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayerState : State
{
    public NavMeshAgent agent;
    public int noticeRange = 15;
    public float stopRange = 1.5f;
    [SerializeField]
    private Transform player;

    public override void Enter() { }

    public override void Exit() { }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= noticeRange && Vector3.Distance(player.position, transform.position) >= stopRange)
        {
            agent.SetDestination(player.position);
        }
        if (Vector3.Distance(player.position, transform.position) <= stopRange)
        {
            agent.SetDestination(transform.position);
            context.TransitionTo((int)BossAI.StateOptions.Attacking);
        }
    }

}

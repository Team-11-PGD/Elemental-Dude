using UnityEngine.AI;
using UnityEngine;

public class SlimeMove : SlimeState
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float stopRange = 1.5f;

    public override void Enter(int previousStateId)
    {
        base.Enter(previousStateId);
    }

    public override void Exit(int nextStateId) { }

    void Update()
    {
        agent.SetDestination(slimeAI.playerModel.position);
        if (Vector3.Distance(slimeAI.playerModel.position, transform.position) <= stopRange)
        {
            agent.SetDestination(transform.position);
            context.TransitionTo(EnemyAI.StateOptions.Attacking);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveToPlayerState : EnemyState
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float stopRange = 1.5f;

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit() { }

    void Update()
    {
        agent.SetDestination(enemyAI.playerModel.position);
        if (Vector3.Distance(enemyAI.playerModel.position, transform.position) <= stopRange)
        {
            agent.SetDestination(transform.position);
            switch (Random.Range(0, 2))
            {
                case 0:
                    context.TransitionTo((int)BossAIOld.StateOptions.FireAttacking1);
                    break;
                case 1:
                    context.TransitionTo((int)BossAIOld.StateOptions.FireAttacking2);
                    break;
            }

        }
    }

}

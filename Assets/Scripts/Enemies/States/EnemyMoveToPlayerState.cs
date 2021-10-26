using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveToPlayerState : State
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float stopRange = 1.5f;
    EnemyAI enemyAI;

    public override void Enter()
    {
        enemyAI = context as EnemyAI;
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
                    context.TransitionTo((int)BossAI.StateOptions.FireAttacking1);
                    break;
                case 1:
                    context.TransitionTo((int)BossAI.StateOptions.FireAttacking2);
                    break;
            }

        }
    }

}

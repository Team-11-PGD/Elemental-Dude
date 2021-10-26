using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossMoveToPlayerState : State
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float stopRange = 1.5f;
    BossAI bossAI;

    public override void Enter()
    {
        bossAI = context as BossAI;
    }

    public override void Exit() { }

    void Update()
    {
        agent.SetDestination(bossAI.playerModel.position);
        if (Vector3.Distance(bossAI.playerModel.position, transform.position) <= stopRange)
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

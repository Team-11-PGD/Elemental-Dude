using System.Collections;
using UnityEngine;

public class BossMoveToPlayerState : State
{
    [SerializeField]
    float stopRange = 1.5f;
    BossAI bossAI;

    public override void Enter(int previousStateId)
    {
        bossAI = context as BossAI;
    }

    public override void Exit(int nextStateId)
    {
        bossAI.agent.SetDestination(transform.position);
    }

    void Update()
    {
        bossAI.agent.SetDestination(bossAI.playerModel.position);
        if (Vector3.Distance(bossAI.playerModel.position, transform.position) <= stopRange)
        {
            bossAI.NextState();
        }
    }

}

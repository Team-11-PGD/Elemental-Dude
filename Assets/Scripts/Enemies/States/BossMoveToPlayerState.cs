using System.Collections;
using UnityEngine;

public class BossMoveToPlayerState : State
{
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
        bossAI.agent.SetDestination(bossAI.playerModel.position);
        if (Vector3.Distance(bossAI.playerModel.position, transform.position) <= stopRange)
        {
            bossAI.agent.SetDestination(transform.position);
            bossAI.NextState();
        }
    }

}

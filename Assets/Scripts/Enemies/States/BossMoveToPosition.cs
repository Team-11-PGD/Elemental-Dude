using System.Collections;
using UnityEngine;

public class BossMoveToPosition : State
{
    [SerializeField]
    float stopRange = 1.5f;
    [SerializeField]
    Transform target;

    BossAI bossAI;

    public override void Enter(int previousStateId)
    {
        bossAI = context as BossAI;
        bossAI.agent.SetDestination(target.position);
    }

    public override void Exit(int nextStateId) { }

    public void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= stopRange)
        {
            bossAI.agent.SetDestination(transform.position);
            bossAI.NextState();
        }
    }
}

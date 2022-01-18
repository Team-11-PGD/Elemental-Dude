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
        //bossAI.agent.SetDestination(target.position);
        bossAI.animator.speed = 1.25f;

            bossAI.bossTargeting.SetTarget(target);
        if (Physics.Raycast(target.position, Vector3.down, out RaycastHit hit, 100))
        {
        }
    }

    public override void Exit(int nextStateId)
    {
        //bossAI.agent.SetDestination(transform.position);
        bossAI.bossTargeting.ClearTarget();
        bossAI.bossTargeting.HasArrived = true;
        bossAI.animator.speed = 1f;
    }

    public void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= stopRange)
        {
            bossAI.NextState();
        }
    }
}

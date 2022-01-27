using UnityEngine;

public class BossMoveToPlayerState : FireBossState
{
    [SerializeField]
    float stopRange = 1.5f;

    public override void Enter(int previousStateId)
    {
        BossAI.animator.speed = 1.25f;

        BossAI.bossTargeting.SetTarget(BossAI.playerModel);
    }

    public override void Exit(int nextStateId)
    {
        BossAI.bossTargeting.ClearTarget();
        BossAI.bossTargeting.HasArrived = true;
        BossAI.animator.speed = 1f;
    }

    void Update()
    {
        if (Vector3.Distance(BossAI.playerModel.position, transform.position) <= stopRange)
        {
            BossAI.NextState();
        }
    }

}

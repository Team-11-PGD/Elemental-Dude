using System.Collections;
using UnityEngine;

public class BossMoveToPlayerState : State
{
    [SerializeField]
    float stopRange = 1.5f;
    [SerializeField]
    [Range(-1, 1)]
    float lookPrecision = 0.9f;
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
        Vector3 playerDirection = bossAI.playerModel.position - bossAI.transform.position;
        float playerDotLook = Vector2.Dot(new Vector2(bossAI.transform.forward.x, bossAI.transform.forward.z).normalized, new Vector2(playerDirection.x, playerDirection.z).normalized);
        //Debug.Log($"{playerDirection} => {playerDotLook}");
        if (Vector3.Distance(bossAI.playerModel.position, transform.position) <= stopRange && playerDotLook >= lookPrecision)
        {
            bossAI.NextState();
        }
    }

}

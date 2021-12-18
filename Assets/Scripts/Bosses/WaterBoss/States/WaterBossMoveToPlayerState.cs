using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class WaterBossMoveToPlayerState : State
{
    WaterBossAI bossAI;

    [SerializeField]
    private float rangeAroundTarget = 5;

    public override void Enter(int previousStateId)
    {
        bossAI = context as WaterBossAI;
        transform.position = CalculateTeleportPosition(bossAI.playerModel);
    }

    public override void Exit(int nextStateId) { }


    private Vector3 CalculateTeleportPosition(Transform targetTransform)
    {
        Vector3 teleportDirection = targetTransform.transform.position - transform.position;
        Vector3 lengthShortner = teleportDirection.normalized * rangeAroundTarget;
        return teleportDirection - lengthShortner;
    }
}

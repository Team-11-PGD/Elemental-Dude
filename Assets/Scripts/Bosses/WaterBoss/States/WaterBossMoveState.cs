using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class WaterBossMoveState : State
{
    private WaterBossAI bossAI;

    [SerializeField]
    private List<Transform> teleportPositions;

    public bool teleportToPlayer;

    [SerializeField]
    private float rangeAroundTarget = 5;

    public override void Enter(int previousStateId)
    {
        bossAI = context as WaterBossAI;
        if (teleportToPlayer) transform.position = CalculateTeleportPosition(bossAI.playerModel);
        else transform.position =  CalculateTeleportPosition(teleportPositions[Random.Range(0, teleportPositions.Count - 1)]);
        //Only teleports to a position that is connected to the navmesh it's currently on.
        bossAI.NextState();
    }
    
    public override void Exit(int nextStateId) { }


    private Vector3 CalculateTeleportPosition(Transform targetTransform)
    {
        Vector3 teleportDirection = targetTransform.transform.position - transform.position;
        Vector3 lengthShortner = teleportDirection.normalized * rangeAroundTarget;
        return teleportDirection - lengthShortner;
    }
}

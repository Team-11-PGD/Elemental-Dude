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

        //if (transform.localScale.x != 3.5f) transform.localScale *= 3.5f;

        //StartCoroutine(Teleport());

        if (teleportToPlayer) transform.position = CalculateTeleportPosition(bossAI.playerModel);
        else transform.position = teleportPositions[Random.Range(0, teleportPositions.Count - 1)].position;

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

    private IEnumerator Teleport()
    {

        bossAI.animal.Mode_Activate(4, 35);

        yield return new WaitForSeconds(2);

        if (teleportToPlayer) transform.position = CalculateTeleportPosition(bossAI.playerModel);
        else transform.position = teleportPositions[Random.Range(0, teleportPositions.Count - 1)].position;
    }
}

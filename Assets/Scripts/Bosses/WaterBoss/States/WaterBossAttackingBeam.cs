using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossAttackingBeam : State
{
    WaterBossAI bossAI;

    [SerializeField]
    GameObject waterBeam;

    [SerializeField]
    private float distanceFromBoss;

    public override void Enter(int previousStateId)
    {
        bossAI = context as WaterBossAI;
        FireBeam();
    }

    public override void Exit(int nextStateId)
    {
        bossAI.facePlayer = false;
    }

    private void FireBeam()
    {
        GameObject beamInstance = Instantiate(waterBeam, bossAI.beamFirePoint.position, Quaternion.identity);
        beamInstance.GetComponent<WaterBeam>().BeamTarget(bossAI.beamFirePoint, bossAI.playerModel, bossAI.beamEndPoint);
    }
}

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
    // Start is called before the first frame update
    public override void Enter(int previousStateId)
    {
        bossAI = context as WaterBossAI;
        bossAI.facePlayer = true;
    }

    public override void Exit(int nextStateId)
    {
        bossAI.facePlayer = false;
    }

    void UseBeam()
    {
        GameObject beamInstance = Instantiate(waterBeam);

    }
}

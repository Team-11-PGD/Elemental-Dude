using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossAttackingWave : State
{
    WaterBossAI bossAI;

    [SerializeField]
    GameObject waterWave;

    [SerializeField]
    private float distanceFromBoss;
    public override void Enter(int previousStateId)
    {
        bossAI = context as WaterBossAI;
        bossAI.facePlayer = true;
        SpawnWave();
        bossAI.NextState();
    }

    public override void Exit(int nextStateId)
    {
        bossAI.facePlayer = false;
    }

    private void SpawnWave()
    {
        GameObject waveInstance = Instantiate(waterWave, transform.position + Vector3.forward * distanceFromBoss, this.transform.rotation);
        waveInstance.GetComponent<WaterWaveScript>().GiveTarget(this.gameObject.transform, bossAI.playerModel);
    }
}

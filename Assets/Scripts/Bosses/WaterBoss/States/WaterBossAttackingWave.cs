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
        StartCoroutine(SpawnWave());
        bossAI.NextState();
    }

    public override void Exit(int nextStateId)
    {

    }

    private IEnumerator SpawnWave()
    {
        GameObject waveInstance = Instantiate(waterWave, transform.position + Vector3.forward * distanceFromBoss, this.transform.rotation);
        waveInstance.GetComponent<WaterWaveScript>().GiveTarget(this.gameObject.transform, bossAI.playerModel);

        yield return new WaitForSeconds(bossAI.stateCooldown);
    }
}

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
        StartCoroutine(FireBeam());
        bossAI.NextState();
    }

    public override void Exit(int nextStateId)
    {
        bossAI.facePlayer = false;
    }

    private IEnumerator FireBeam()
    {
        int direction = Random.Range(0, 2);
        yield return new WaitForSecondsRealtime(0.5f);
        if(direction < 1)
        {
            GameObject beamInstance = Instantiate(waterBeam, bossAI.beamFirePoint.position, Quaternion.identity);
            beamInstance.GetComponent<WaterBeam>().BeamTarget(bossAI.beamFirePoint, bossAI.playerModel, bossAI.beamEndPointLeft);
            yield return new WaitForSecondsRealtime(3);
            Destroy(beamInstance);
        }
        if(direction >= 1)
        {
            GameObject beamInstance = Instantiate(waterBeam, bossAI.beamFirePoint.position, Quaternion.identity);
            beamInstance.GetComponent<WaterBeam>().BeamTarget(bossAI.beamFirePoint, bossAI.playerModel, bossAI.beamEndPointRight);
            yield return new WaitForSecondsRealtime(3);
            Destroy(beamInstance);
        }
    }
}

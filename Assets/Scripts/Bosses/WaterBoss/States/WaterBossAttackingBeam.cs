using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossAttackingBeam : State
{
    WaterBossAI bossAI;

    [SerializeField]
    Transform beamFirePoint, beamEndPointLeft, beamEndPointRight, beamTarget;

    [SerializeField]
    GameObject waterBeam;
    [SerializeField]
    GameObject chargeEffect;
    [SerializeField]
    float chargeTime = 2f;
    [SerializeField]
    float beamduration = 3f;

    public override void Enter(int previousStateId)
    {
        bossAI = context as WaterBossAI;
        beamTarget = bossAI.playerModel;
        StartCoroutine(FireBeam());
    }

    public override void Exit(int nextStateId)
    {

    }

    private IEnumerator FireBeam()
    {
        beamTarget.position = bossAI.playerModel.position;
        //visual cue for player
        GameObject charge = Instantiate(chargeEffect, beamFirePoint.position, context.transform.rotation, context.transform);
        int direction = Random.Range(0, 2);
        yield return new WaitForSecondsRealtime(chargeTime);
        Destroy(charge);
        GameObject beamInstance = Instantiate(waterBeam, beamFirePoint.position, Quaternion.identity);
        //beam travels to the left or right
        if (direction < 1)
        {
            beamInstance.GetComponent<WaterBeam>().BeamTarget(beamFirePoint, beamTarget, beamEndPointLeft);
            yield return new WaitForSecondsRealtime(beamduration);
            Destroy(beamInstance);
        }
        if(direction >= 1)
        {
            
            beamInstance.GetComponent<WaterBeam>().BeamTarget(beamFirePoint, beamTarget, beamEndPointRight);
            yield return new WaitForSecondsRealtime(beamduration);
            Destroy(beamInstance);
        }
        bossAI.NextState();
        yield return new WaitUntil(() => beamInstance = null) ;
    }
}

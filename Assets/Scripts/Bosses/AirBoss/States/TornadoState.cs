using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoState : State
{
    [SerializeField]
    GameObject tornadoPrefab;
    [SerializeField]
    GameObject testCube;
    [SerializeField]
    Transform tornadoCenter;
    [SerializeField]
    float tornadoTime = 10;
    [SerializeField]
    float pullForce = 500;
    float refreshRate;

    AirBossAI bossAI;

    public override void Enter(int previousStateId)
    {
        bossAI = context as AirBossAI;
        StartCoroutine(PullPlayer());
    }

    public override void Exit(int nextStateId)
    {

    }

    public void Update()
    {
        Vector3 testPullDirection = tornadoCenter.position - testCube.transform.position;
        testCube.GetComponent<Rigidbody>().AddForce(testPullDirection.normalized * pullForce * Time.deltaTime);
    }

    IEnumerator PullPlayer()
    {
        Vector3 pullDirection = tornadoCenter.position - bossAI.playerModel.position;
        Instantiate(tornadoPrefab, tornadoCenter.position, context.transform.rotation);
        yield return new WaitForSecondsRealtime(tornadoTime);
        bossAI.TransitionTo(AirBossAI.StateOptions.Dash);
        //bossAI.playerModel.GetComponent<Rigidbody>().AddForce(pullDirection.normalized * pullForce * Time.deltaTime);
    }
}

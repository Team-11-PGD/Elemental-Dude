using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoState : State
{
    [SerializeField]
    GameObject tornadoPrefab;
    [SerializeField]
    Transform tornadoCenter;
    [SerializeField]
    float tornadoTime = 5;

    AirBossAI bossAI;

    public override void Enter(int previousStateId)
    {
        bossAI = context as AirBossAI;
        StartCoroutine(PullPlayer());
    }

    public override void Exit(int nextStateId)
    {
        
    }

    IEnumerator PullPlayer()
    {
        yield return new WaitForSecondsRealtime(tornadoTime);
        Instantiate(tornadoPrefab, tornadoCenter.position, tornadoCenter.rotation, tornadoCenter.transform);
      /*  tornado.GetComponent<GameObjectRemover>().ShutDown();
        bossAI.TransitionTo(AirBossAI.StateOptions.Dash);*/
        
    }
}

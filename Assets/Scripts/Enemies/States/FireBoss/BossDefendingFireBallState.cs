using System.Collections;
using UnityEngine;

public class BossDefendingFireBallState : State
{
    FireBossAI bossAI;

    public override void Enter()
    {
        bossAI = context as FireBossAI;
        StartCoroutine(Timer(2));
    }

    public override void Exit() { }

    public void Update()
    {
        context.transform.Rotate(Vector3.up, 2);
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        bossAI.NextDefendState();
    }
}

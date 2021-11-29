using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    [SerializeField]
    float attackRange = 1;
    AirBossAI bossAI;
    Vector3 dashDirection;

    public override void Enter(int previousStateId)
    {
        bossAI = context as AirBossAI;
        StartCoroutine(Dash());
    }

    private void Update()
    {

    }

    IEnumerator Dash()
    {
        dashDirection = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        Coroutine timer = StartCoroutine(Timer());
        while (Vector3.Distance(transform.position, bossAI.playerModel.position) > attackRange || timer == null)
        {
            yield return null;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(2);
    }
}

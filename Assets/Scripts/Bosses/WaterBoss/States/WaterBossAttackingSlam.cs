using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossAttackingSlam : State
{
    [SerializeField]
    float chargeTime = 1.8f;
    [SerializeField]
    GameObject slamPrefab;
    [SerializeField]
    Transform slamPosition;
    WaterBossAI bossAI;

    public override void Enter(int previousStateId)
    {
        bossAI = context as WaterBossAI;
        bossAI.facePlayer = true;
        StartCoroutine(SlamAttack());
    }

    public override void Exit(int nextStateId)
    {
        bossAI.facePlayer = false;
    }

    IEnumerator SlamAttack()
    {
        Instantiate(slamPrefab, slamPosition.position, context.transform.rotation, context.transform);
        //SOUND: (slam)
        yield return new WaitForSecondsRealtime(chargeTime);
    }
}

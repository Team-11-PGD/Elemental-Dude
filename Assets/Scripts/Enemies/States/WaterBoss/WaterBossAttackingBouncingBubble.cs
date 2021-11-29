using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossAttackingBouncingBubble : State
{
    WaterBossAI bossAI;

    [SerializeField]
    GameObject bubble;

    [SerializeField]
    private float distanceFromBoss;

    public override void Enter(int previousStateId)
    {
        bossAI = context as WaterBossAI;
        bossAI.facePlayer = true;
        SpawnBubble();
    }

    public override void Exit(int nextStateId)
    {
        bossAI.facePlayer = false;
    }

    private void SpawnBubble()
    {
        GameObject bubbleInstance = Instantiate(bubble, transform.position + Vector3.forward * distanceFromBoss, Quaternion.identity);
        bubbleInstance.GetComponent<BouncingSplittingBubble>().GiveTarget(this.gameObject.transform, bossAI.playerModel);
    }
}

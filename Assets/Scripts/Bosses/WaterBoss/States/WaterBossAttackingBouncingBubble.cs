using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class WaterBossAttackingBouncingBubble : State
{
    private WaterBossAI bossAI;

    [SerializeField]
    private GameObject bubble;

    [SerializeField]
    private float distanceFromBoss;

    public override void Enter(int previousStateId)
    {
        bossAI = context as WaterBossAI;
        bossAI.facePlayer = true;
        StartCoroutine(SpawnBubble());
        bossAI.NextState();
    }

    public override void Exit(int nextStateId) { }

    /// <summary>
    /// Spawn a bubble instance moving from boss to player.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnBubble()
    {
        GameObject bubbleInstance = Instantiate(bubble, transform.position + Vector3.forward * distanceFromBoss, Quaternion.identity);
        bubbleInstance.GetComponent<BouncingSplittingBubble>().GiveTarget(this.gameObject.transform, bossAI.playerModel);

        yield return new WaitForSeconds(bossAI.stateCooldown);
    }
}

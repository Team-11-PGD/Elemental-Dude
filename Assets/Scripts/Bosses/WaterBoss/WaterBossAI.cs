using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterBossMoveState), typeof(WaterBossAttackingBouncingBubble), typeof(WaterBossAttackingSlam))]
[RequireComponent(typeof(WaterBossAttackingWave))]

public class WaterBossAI : BossAI
{
    public bool facePlayer = false;

    public enum StateOptions
    {
        MoveToPlayer,
        WaterAttackBubble,
        WaterAttackSlam,
        WaterAttackWave,
        WaterAttackBeam,
        MoveToCenter,
        WaterDefenceWall,
        WaterDefenceDome,
        Death
    }

    [SerializeField]
    protected StateOptions startState;

    private void Start()
    {
        AddState(StateOptions.MoveToPlayer, GetComponent<WaterBossMoveState>());                     // 0
        AddState(StateOptions.WaterAttackBubble, GetComponent<WaterBossAttackingBouncingBubble>());          // 1
        AddState(StateOptions.WaterAttackSlam, GetComponent<WaterBossAttackingSlam>());                      // 2
        AddState(StateOptions.WaterAttackWave, GetComponent<WaterBossAttackingWave>());                      // 3
        StateMachineSetup(startState);
    }

    private void Update()
    {
        if (facePlayer)
        {
            Vector3 playerPosition = new Vector3(playerModel.transform.position.x, transform.position.y, playerModel.transform.position.z);
            transform.LookAt(playerPosition);
            //TODO: (make boss turn slowly instead of snap to player)
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterBossMoveToPlayerState))]

public class WaterBossAI : BossAI
{
    public bool facePlayer = false;

    public enum StateOptions
    {
        MoveToPlayer,
        WaterAttackWave,
        WaterAttackBubble,
        WaterAttackBeam,
        WaterAttackSlam,
        MoveToCenter,
        WaterDefenceWall,
        WaterDefenceDome,
        Death
    }

    [SerializeField]
    protected StateOptions startState;

    private void Start()
    {
        states.Add((int)StateOptions.MoveToPlayer, GetComponent<WaterBossMoveToPlayerState>());          // 0

        StateMachineSetup((int)startState);
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

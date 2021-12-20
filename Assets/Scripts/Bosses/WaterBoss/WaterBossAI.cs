using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterBossMoveState), typeof(WaterBossAttackingBouncingBubble), typeof(WaterBossAttackingSlam))]
[RequireComponent(typeof(WaterBossAttackingWave))]

public class WaterBossAI : BossAI
{
    public bool facePlayer = false;
    public Transform beamFirePoint;
    public Transform beamEndPoint;
    [SerializeField]
    float defenceStatePercentage = 0.4f;
    [SerializeField]
    float waterRisePercentage = 0.8f;

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
        AddState(StateOptions.WaterAttackBeam, GetComponent<WaterBossAttackingBeam>());                      // 4
        StateMachineSetup(startState);
    }

    public override void NextState()
    {
        switch (CurrentStateId)
        {

        }
    }

    public void NextAttackState()
    {
        if (!SwitchToDefend()) TransitionTo(RandomStateFromRange(StateOptions.FireAttacking1, StateOptions.FireAttacking2));
    }

    public bool SwitchToDefend()
    {
        if (health.HpPercentage <= waterRisePercentage)
        {
            TransitionTo(StateOptions.MoveToCenter);
            return true;
        }
        if(health.HpPercentage <= defenceStatePercentage)
        {
            TransitionTo(StateOptions.)
        }
        return false;
    }

    public void NextDefendState()
    {
        if (shieldHealth.HpPercentage > 0) TransitionTo(RandomStateFromRange(StateOptions.Defending1, StateOptions.Defendig2));
        else
        {
            TransitionTo(StateOptions.MoveToPlayer);
        }
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

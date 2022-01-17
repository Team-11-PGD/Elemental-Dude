using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(WaterBossMoveState), typeof(WaterBossAttackingBouncingBubble), typeof(WaterBossAttackingSlam))]
[RequireComponent(typeof(WaterBossAttackingWave))]

public class WaterBossAI : BossAI
{
    public bool facePlayer = false;

    [SerializeField]
    float defenceStatePercentage = 0.4f;
    [SerializeField]
    float waterRisePercentage = 0.8f;
    [SerializeField]
    private WaterController controller;

    private float distanceToPlayer;
    [SerializeField]
    float meleeRange;
    [SerializeField]
    int nextStateDelay;

    public enum StateOptions
    {
        MoveToPlayer,
        WaterAttackSlam,
        WaterAttackWave,
        WaterAttackBubble,
        WaterAttackBeam,
        Death
    }

    [SerializeField]
    protected StateOptions startState;

    private void Start()
    {
        AddState(StateOptions.MoveToPlayer, GetComponent<WaterBossMoveState>());                             // 0
        AddState(StateOptions.WaterAttackSlam, GetComponent<WaterBossAttackingSlam>());                      // 1
        AddState(StateOptions.WaterAttackWave, GetComponent<WaterBossAttackingWave>());                      // 2
        AddState(StateOptions.WaterAttackBubble, GetComponent<WaterBossAttackingBouncingBubble>());          // 3
        AddState(StateOptions.WaterAttackBeam, GetComponent<WaterBossAttackingBeam>());                      // 4
        StateMachineSetup(startState);
    }
    public new async void TransitionTo(Enum nextState)
    {
        await Task.Delay(nextStateDelay);
        base.TransitionTo(nextState);
    }
    public override void NextState()
    {
        switch (CurrentStateId)
        {
            case (int)StateOptions.WaterAttackSlam:
                NextAttackState();
                break;
            case (int)StateOptions.WaterAttackWave:
                NextAttackState();
                break;
            case (int)StateOptions.WaterAttackBubble:
                TransitionTo(StateOptions.MoveToPlayer);
                break;
            case (int)StateOptions.WaterAttackBeam:
                TransitionTo(StateOptions.MoveToPlayer);
                break;
            case (int)StateOptions.MoveToPlayer:
                NextAttackState();
                break;
        }
    }


    public void NextAttackState()
    {
        if (distanceToPlayer <= meleeRange)
        {
            TransitionTo(RandomStateFromRange(StateOptions.WaterAttackSlam, StateOptions.WaterAttackWave));
        }
        if(distanceToPlayer > meleeRange)
        {
            TransitionTo(RandomStateFromRange(StateOptions.WaterAttackBubble, StateOptions.WaterAttackBeam));
        }
    }

    public bool SwitchToDefend()
    {
        if (health.HpPercentage <= waterRisePercentage && health.HpPercentage > defenceStatePercentage)
        {
            controller.startAppear = true;
            return true;
        }
        if(health.HpPercentage <= defenceStatePercentage)
        {
            controller.start2 = true;
            GetComponent<WaterBossMoveState>().teleportToPlayer = false;
            TransitionTo(StateOptions.MoveToPlayer);
            return true;
        }
        return false;
    }

    protected override void Hitted()
    {
        SwitchToDefend();
    }

    private StateOptions RandomStateFromRange(StateOptions minInclusive, StateOptions maxInclusive)
        => (StateOptions)UnityEngine.Random.Range((int)minInclusive, (int)maxInclusive + 1);

    private void Update()
    {
        distanceToPlayer = (playerModel.position - this.transform.position).magnitude;
        if (facePlayer)
        {
            Vector3 playerPosition = new Vector3(playerModel.transform.position.x, transform.position.y, playerModel.transform.position.z);
            transform.LookAt(playerPosition);
            //TODO: (make boss turn slowly instead of snap to player)
        }
    }
}

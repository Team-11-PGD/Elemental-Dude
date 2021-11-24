using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossMoveToPlayerState), typeof(BossLavaSlamAttack), typeof(BossFlameBreathAttack))]
[RequireComponent(typeof(BossDefendingFireBallState), typeof(BossDefendingLavaStreamState), typeof(BossMoveToPosition))]
[RequireComponent(typeof(BossDeath))]
public class FireBossAI : BossAI
{
    [SerializeField]
    Health shieldHealth;
    [SerializeField]
    GameObject shield;
    [SerializeField]
    [Range(0, 1)]
    float nextPercentageStep = 0.33f;
    [SerializeField]
    [Range(0, 1)]
    float nextStatePercentage = 0.66f;

    public enum StateOptions
    {
        MoveToPlayer,
        FireAttacking1,
        FireAttacking2,
        MoveToCenter,
        Defending1,
        Defendig2,
        Death
    }

    [SerializeField]
    protected StateOptions startState;

    protected void Start()
    {
        states.Add((int)StateOptions.MoveToPlayer, GetComponent<BossMoveToPlayerState>());          // 0
        states.Add((int)StateOptions.FireAttacking1, GetComponent<BossLavaSlamAttack>());           // 1
        states.Add((int)StateOptions.FireAttacking2, GetComponent<BossFlameBreathAttack>());        // 2
        states.Add((int)StateOptions.MoveToCenter, GetComponent<BossMoveToPosition>());             // 3
        states.Add((int)StateOptions.Defending1, GetComponent<BossDefendingFireBallState>());       // 4
        states.Add((int)StateOptions.Defendig2, GetComponent<BossDefendingLavaStreamState>());      // 5
        states.Add((int)StateOptions.Death, GetComponent<BossDeath>());                             // 6

        StateMachineSetup((int)startState);
    }

    void OnEnable()
    {
        health.Hitted += Hitted;
        shieldHealth.Hitted += ShieldHitted;
    }

    void OnDisable()
    {
        health.Hitted -= Hitted;
        shieldHealth.Hitted -= ShieldHitted;
    }

    public override void NextState()
    {
        switch (CurrentStateId)
        {
            case (int)StateOptions.FireAttacking1:
            case (int)StateOptions.FireAttacking2:
                if (!SwitchToDefend()) TransitionTo((int)StateOptions.MoveToPlayer);
                break;
            case (int)StateOptions.MoveToPlayer:
                NextAttackState();
                break;
            case (int)StateOptions.Defending1:
            case (int)StateOptions.Defendig2:
            case (int)StateOptions.MoveToCenter:
                health.enabled = true;
                shieldHealth.enabled = true;
                NextDefendState();
                break;
        }
    }

    void Hitted()
    {
        if (health.HpPercentage <= 0)
        {
            TransitionTo((int)StateOptions.Death);
            //SOUND: (boss death sound)
        }
        else
        {
            SwitchToDefend();
        }
    }

    void ShieldHitted()
    {
        if (shieldHealth.HpPercentage <= 0)
        {
            TransitionTo((int)StateOptions.MoveToPlayer);
        }
    }

    public void NextAttackState()
    {
        if (!SwitchToDefend()) TransitionTo(Random.Range(1, 3));
    }

    public bool SwitchToDefend()
    {
        if (health.HpPercentage <= nextStatePercentage)
        {
            nextStatePercentage -= nextPercentageStep;
            TransitionTo((int)StateOptions.MoveToCenter);
            health.enabled = false;
            shield.SetActive(true);
            shieldHealth.currentHp = shieldHealth.maxHp;
            shieldHealth.enabled = false;
            return true;
        }
        shield.SetActive(false);
        return false;
    }

    public void NextDefendState()
    {
        if (shieldHealth.HpPercentage > 0) TransitionTo(Random.Range(4, 6));
        else
        {
            TransitionTo((int)StateOptions.MoveToPlayer);
        }
    }

    private void FixedUpdate()
    {
        if (CurrentStateId == 3)
        {
            shield.tag = "UnhittableShield";
        }
        else shield.tag = "BossShield";
    }


}

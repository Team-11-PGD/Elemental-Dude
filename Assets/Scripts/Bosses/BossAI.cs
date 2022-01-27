using System;
using UnityEngine;
using MalbersAnimations.Controller.AI;
using MalbersAnimations.Controller;
using System.Collections;

// Joshua Knaven
public class BossAI : StateMachine
{
    public Transform playerModel;
    public Health playerHealth;
    public MAnimalAIControl bossTargeting;
    public Animator animator;
    public MAnimal animal;
    public Health health;

    protected int activeTime = 0;

    protected new void TransitionTo(Enum nextState)
    {
        if (CurrentState is BossState)
        {
            health.Hitted -= (CurrentState as BossState).Hitted;
            health.Died -= (CurrentState as BossState).Died;
        }
        base.TransitionTo(nextState);
        if (CurrentState is BossState)
        {
            health.Hitted += (CurrentState as BossState).Hitted;
            health.Died += (CurrentState as BossState).Died;
        }
    }

    private void Update()
    {
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        yield return new WaitForSecondsRealtime(0);
        activeTime++;
    }

    protected virtual void OnEnable()
    {
        health.Hitted += Hitted;
        health.Died += Died;
    }

    protected virtual void OnDisable()
    {
        health.Hitted -= Hitted;
        health.Died -= Died;
    }

    protected virtual void Hitted() { }
    protected virtual void Died() { }
    public virtual void NextState() { }
}


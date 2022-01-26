using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Joshua Knaven
public class BossAI : StateMachine
{
    //public NavMeshAgent agent;
    public Transform playerModel;
    public Health playerHealth;

    [SerializeField]
    public MalbersAnimations.Controller.AI.MAnimalAIControl bossTargeting;

    [SerializeField]
    public Animator animator;

    [SerializeField]
    public MalbersAnimations.Controller.MAnimal animal;

    [SerializeField]
    protected Health health;

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


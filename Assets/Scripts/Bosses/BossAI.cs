using System;
using UnityEngine;
using MalbersAnimations.Controller.AI;
using MalbersAnimations.Controller;

// Joshua Knaven
public class BossAI : StateMachine
{
    public Transform playerModel;
    public Health playerHealth;

    [SerializeField]
    protected MAnimalAIControl bossTargeting;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected MAnimal animal;
    [SerializeField]
    protected Health health;

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


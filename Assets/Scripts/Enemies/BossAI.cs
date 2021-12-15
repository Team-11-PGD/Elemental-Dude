using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : StateMachine
{
    public NavMeshAgent agent;
    public Transform playerModel;
    public Health playerHealth;

    [SerializeField]
    protected Health health;

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


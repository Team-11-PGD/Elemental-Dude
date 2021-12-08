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

    public virtual void NextState() { }
}


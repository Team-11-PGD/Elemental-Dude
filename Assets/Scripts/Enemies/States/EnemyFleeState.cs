﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class EnemyFleeState : State
{
    [SerializeField]
    Transform player;
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float fleeRange = 30;

    bool cornerCheck = false;

    public override void Enter()
    {
        NewFleeDestination();
        StartCoroutine(CornerTimer());
    }

    public override void Exit() { }

    void NewFleeDestination()
    {
        Vector3 dirToPlayer = player.position - transform.position;
        Vector3 fleePos = transform.position - dirToPlayer;
        agent.SetDestination(fleePos);
    }

    void Update()
    {
        Debug.Log($"{name} is Fleeing");

        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            NewFleeDestination();
        }

        if (Vector3.Distance(player.position, transform.position) >= fleeRange || (agent.velocity == Vector3.zero && cornerCheck))
        {
            context.TransitionTo((int)EnemyAI.StateOptions.Heal);
        }
    }

    IEnumerator CornerTimer()
    {
        yield return new WaitForSecondsRealtime(1);
        cornerCheck = true;
    }
}


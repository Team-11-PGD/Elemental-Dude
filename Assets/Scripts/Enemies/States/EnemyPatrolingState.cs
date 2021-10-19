using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class EnemyPatrolingState : State
{
    [SerializeField]
    Transform player;
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    public int noticeRange = 15;
    [SerializeField]
    float patrolingRange = 15;
    [SerializeField]
    Health enemyHealth;

    float normalAgentSpeed;

    public override void Enter()
    {
        normalAgentSpeed = agent.speed;
        agent.speed = 3.5f;
    }

    public override void Exit()
    {
        agent.speed = normalAgentSpeed;
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= noticeRange || enemyHealth.HpPercentage != 1)
        {
            context.TransitionTo((int)EnemyAI.StateOptions.MoveToPlayer);
        }
        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(PatrolingPosition());
        }
    }

    public Vector3 PatrolingPosition()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = UnityEngine.Random.insideUnitSphere * patrolingRange;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, patrolingRange, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}


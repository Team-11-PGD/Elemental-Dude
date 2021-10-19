using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavControler : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;
    public int noticeRange = 15;
    public float stopRange = 1.5f;
    public float fleeRange = 30;
    public float wanderRange = 15;
    public float currentRange;
    Vector3 fleePos;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentRange = Vector3.Distance(player.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirToPlayer = transform.position - player.position;
        fleePos = transform.position + dirToPlayer;
        currentRange = Vector3.Distance(player.position, transform.position);
    }
    public void Stop()
    {
        agent.SetDestination(transform.position);
    }
    public void Approach()
    {
        agent.speed = 5f;
        agent.SetDestination(player.position);
    }

    public void Flee()
    {
        if(currentRange < fleeRange)
        {
            agent.SetDestination(fleePos);
        }
    }

    public Vector3 WanderArea()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * wanderRange;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, wanderRange, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void Wander()
    {
        agent.speed = 3.5f;
        if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(WanderArea());
        }
    }
}

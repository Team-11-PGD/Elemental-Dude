using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeIdle : SlimeState
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    public int noticeRange = 15;
    [SerializeField]
    float patrolingRange = 15;
    [SerializeField]
    Health enemyHealth;

    float normalAgentSpeed;

    public override void Enter(int previousStateId)
    {
        base.Enter(previousStateId);
        normalAgentSpeed = agent.speed;
        agent.speed = 3.5f;
    }

    public override void Exit(int nextStateId)
    {
        agent.speed = normalAgentSpeed;
    }

    void Update()
    {
        if (Vector3.Distance(slimeAI.playerModel.transform.position, transform.position) <= noticeRange || enemyHealth.HpPercentage != 1)
        {
            context.TransitionTo(EnemyAI.StateOptions.MoveToPlayer);
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

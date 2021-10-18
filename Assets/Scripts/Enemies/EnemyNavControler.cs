using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavControler : MonoBehaviour
{
    public NavMeshAgent agent;
    public int noticeRange = 15;
    public float stopRange = 1.5f;
    [SerializeField]
    private Transform player;

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= noticeRange && Vector3.Distance(player.position, transform.position) >= stopRange)
        {
            agent.SetDestination(player.position);
        }
        if (Vector3.Distance(player.position, transform.position) <= stopRange)
        {
            agent.SetDestination(transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestContoler : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;
    public int noticeRange = 15;
    public float stopRange = 1.5f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
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

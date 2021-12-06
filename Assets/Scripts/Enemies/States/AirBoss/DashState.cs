using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DashState : State
{
    [SerializeField]
    float attackRange = 1;
    [SerializeField]
    int totalDashes = 5;
    [SerializeField]
    Collider dashArea;
    [SerializeField]
    float damage = 1f;

    AirBossAI bossAI;
    Vector3 dashPosition, dashDirection;
    bool done, move;

    public override void Enter(int previousStateId)
    {
        bossAI = context as AirBossAI;
        done = false;
        Dash();
    }

    public override void Exit(int nextStateId)
    {
        done = true;
    }

    private void Update()
    {
        if (!move) return;
        transform.position += dashDirection;

        if (Vector3.Distance(transform.position, bossAI.playerModel.position) <= attackRange)
        {
            Attack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(dashPosition, 2);
    }

    private async Task Dash()
    {
        int dashCount = 0;

        while (dashCount < totalDashes)
        {
            dashPosition = new Vector3(
                UnityEngine.Random.Range(dashArea.bounds.min.x, dashArea.bounds.max.x),
                UnityEngine.Random.Range(dashArea.bounds.min.y, dashArea.bounds.max.y),
                UnityEngine.Random.Range(dashArea.bounds.min.z, dashArea.bounds.max.z));
            dashDirection = (dashPosition - transform.position).normalized;
            move = true;
            dashCount++;

            while (Vector3.Distance(transform.position, dashPosition) > attackRange) { }
            await Task.Run(Attack);
        }
        move = false;

        dashPosition = bossAI.playerModel.position;
        dashDirection = (dashPosition - transform.position).normalized;

        while (Vector3.Distance(transform.position, bossAI.playerModel.position) > attackRange)
        {
            transform.position += dashDirection;
        }
        await Task.Run(Attack);

        Debug.Log("Stop dash");
        //context.TransitionTo(AirBossAI.StateOptions.DashAttack);
    }

    void Attack()
    {
        //Animation
        //SOUND: hit player
        if (Vector3.Distance(bossAI.playerModel.position, transform.position) <= attackRange)
            bossAI.playerHealth.Hit(damage);
    }
}

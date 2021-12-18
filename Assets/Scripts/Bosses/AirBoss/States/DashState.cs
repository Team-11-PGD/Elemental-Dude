using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    public float speed = 1;

    [SerializeField]
    float attackRange = 1;
    [SerializeField]
    int totalDashes = 5;
    [SerializeField]
    Collider dashArea;
    [SerializeField]
    float damage = 1f;

    AirBossAI bossAI;
    Vector3 dashPosition, dashDirection, dashStartPosition;
    bool move;

    public override void Enter(int previousStateId)
    {
        bossAI = context as AirBossAI;
        StartCoroutine(Dash());
    }

    private void Update()
    {
        if (!move) return;
        transform.position += dashDirection * Time.deltaTime * speed;

        if (Vector3.Dot(transform.position - dashPosition, dashStartPosition - dashPosition) < 0)
        {
            move = false;
            Attack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(dashPosition, 2);
    }

    private IEnumerator Dash()
    {
        int dashCount = 0;

        // Dash randomly
        while (dashCount < totalDashes)
        {
            dashPosition = new Vector3(
                UnityEngine.Random.Range(dashArea.bounds.min.x, dashArea.bounds.max.x),
                UnityEngine.Random.Range(dashArea.bounds.min.y, dashArea.bounds.max.y),
                UnityEngine.Random.Range(dashArea.bounds.min.z, dashArea.bounds.max.z));
            dashStartPosition = transform.position;
            dashDirection = (dashPosition - transform.position).normalized;
            dashCount++;

            move = true;
            yield return new WaitWhile(() => move);
        }

        // Dash towards the player
        dashPosition = bossAI.playerModel.position;
        dashDirection = (dashPosition - transform.position).normalized;

        move = true;
        yield return new WaitWhile(() => move);

        context.NextRandomState<AirBossAI.StateOptions>();
    }

    void Attack()
    {
        //TODO: Animation
        if (Vector3.Distance(bossAI.playerModel.position, transform.position) <= attackRange)
        {
            bossAI.playerHealth.Hit(damage);
            //SOUND: hit player
        }
    }
}

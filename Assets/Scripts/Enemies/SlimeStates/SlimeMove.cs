using UnityEngine.AI;
using UnityEngine;

public class SlimeMove : SlimeState
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float stopRange = 2f;
    [SerializeField]
    Rigidbody rigidbody;
    private bool jumping, executed;
    private float jumpTimer = 1f, deltaTimer;
    private float jumpCooldown, deltaCooldown;
    private float randomValue;
    public int jumpForce = 2000;

    private void Start()
    {
        randomValue = Random.Range(0.5f, 1.1f);
        jumpCooldown = randomValue;
    }
    public override void Enter(int previousStateId)
    {
        base.Enter(previousStateId);
    }

    public override void Exit(int nextStateId) { }

    void Update()
    {
        deltaTimer += Time.deltaTime;
        TimedJumping();// calls for the jump

        if (!jumping)//if slime not jumping it will move to the player.
        {
            agent.SetDestination(slimeAI.playerModel.position);
            if (Vector3.Distance(slimeAI.playerModel.position, transform.position) <= stopRange)
            {
                agent.SetDestination(transform.position);
                context.TransitionTo(EnemyAI.StateOptions.Attacking);
            }
        }
    }
    private void TimedJumping() //timed jumps, will jump in set interfalls. it will disable the agent for a short while.
    {
        if (deltaTimer >= jumpTimer)
        {
            deltaCooldown += Time.deltaTime;
            jumping = true;
            agent.enabled = false;
            if (!executed)
            {
                rigidbody.AddForce(0, jumpForce, 0);
                executed = true;
            }
            if (deltaCooldown >= jumpCooldown) deltaTimer = 0;
        }
        else
        {
            jumping = false;
            executed = false;
            agent.enabled = true;
            deltaCooldown = 0;
        }
    }
}

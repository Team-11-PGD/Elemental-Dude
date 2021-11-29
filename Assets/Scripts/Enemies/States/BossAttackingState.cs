using System.Collections;
using UnityEngine;

public class BossAttackingState: State
{

    [SerializeField]
    Transform player;
    [SerializeField]
    Health playerHealth;
    [SerializeField]
    float meleeDistance = 2;
    [SerializeField]
    float attackChargeTime = 0.5f;
    [SerializeField]
    float damage = 1f;
    public GameObject fireSlam;
    public Transform boss;
    public enum bossAttacks
    {
        fireBreath,
        fireSlam,
        fireballRain,
        lavaStream
    }

    public override void Enter(int previousStateId)
    {
        StartCoroutine(BossAttack());
    }

    public override void Exit(int nextStateId) { }

    public void Update()
    {
        context.transform.Rotate(Vector3.up, 2);
    }

    IEnumerator BossAttack()
    {
        // Play charge animation
        //SOUND: (Charge sound)
        Debug.Log("start bossattack animation");

        yield return new WaitForSecondsRealtime(attackChargeTime);
        if (Vector3.Distance(player.position, transform.position) <= meleeDistance)
        {           
            Instantiate(fireSlam, boss.position, boss.rotation);
            playerHealth.Hit(damage);
            StartCoroutine(BossAttack());
        }
        else
        {
            context.TransitionTo(BossAIOld.StateOptions.MoveToPlayer);
        }
    }

    /*    IEnumerator Timer(float time)
        {
            yield return new WaitForSecondsRealtime(time);

            // You can transition between states with the TransitionTo method.
            // This method requires the id of the state.
            // If you used a enum you will again need to cast this to a int.
            context.TransitionTo(AIExample.StateOptions.EnemyAttacking);
        }*/
}

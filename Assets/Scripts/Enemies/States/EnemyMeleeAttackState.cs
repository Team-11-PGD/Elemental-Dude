using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAttackState : EnemyState
{
    [SerializeField]
    float meleeDistance;
    [SerializeField]
    float attackChargeTime;
    [SerializeField]
    float damage;

    public override void Enter(int previousStateId)
    {
        base.Enter(previousStateId);
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        // Play charge animation
        Debug.Log("start attack animation");

        if (Vector3.Distance(enemyAI.playerModel.position, transform.position) <= meleeDistance)
        {
            AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "EnemyAttack");
            enemyAI.playerHealth.Hit(damage);
            yield return new WaitForSecondsRealtime(attackChargeTime);
            StartCoroutine(Attack());
        }
        else
        {
            context.TransitionTo(EnemyAI.StateOptions.MoveToPlayer);
        }
    }
}

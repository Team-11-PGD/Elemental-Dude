using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : SlimeState
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

        if (Vector3.Distance(slimeAI.playerModel.position, transform.position) <= meleeDistance)
        {
            AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "EnemyAttack");
            slimeAI.playerHealth.Hit(damage);
            yield return new WaitForSecondsRealtime(attackChargeTime);
            StartCoroutine(Attack());
        }
        else
        {
            context.TransitionTo(EnemyAI.StateOptions.MoveToPlayer);
        }
    }
}

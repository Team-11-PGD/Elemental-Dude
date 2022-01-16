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

    [Header("Tmp attack animation")]
    [SerializeField]
    Renderer renderer;
    [SerializeField]
    float animationTime = 0.5f;
    Color startColor;

    public override void Enter(int previousStateId)
    {
        base.Enter(previousStateId);
        startColor = renderer.material.color;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        // Play charge animation
        Debug.Log("start attack animation");
        yield return new WaitForSecondsRealtime(attackChargeTime);

        if (Vector3.Distance(enemyAI.playerModel.position, transform.position) <= meleeDistance)
        {
            // SOUND: Check (Attack)
            AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "EnemyAttack");
            enemyAI.playerHealth.Hit(damage);
            StartCoroutine(TMPAttackAnimation());
            StartCoroutine(Attack());
        }
        else
        {
            context.TransitionTo(EnemyAI.StateOptions.MoveToPlayer);
        }
    }

    IEnumerator TMPAttackAnimation()
    {
        renderer.material.color = Color.white;
        yield return new WaitForSecondsRealtime(animationTime);
        renderer.material.color = startColor;
    }
}

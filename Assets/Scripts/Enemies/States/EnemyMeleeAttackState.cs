using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAttackState : EnemyState
{
    [SerializeField]
    float meleeDistance = 2;
    [SerializeField]
    float attackChargeTime = 1.5f;
    [SerializeField]
    float damage = 1f;

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
        // SOUND: (Atack)
        Debug.Log("start attack animation");

        yield return new WaitForSecondsRealtime(attackChargeTime);
        if (Vector3.Distance(enemyAI.playerModel.position, transform.position) <= meleeDistance)
        {
            enemyAI.playerHealth.Hit(damage);
            StartCoroutine(TMPAttackAnimation());
            StartCoroutine(Attack());
        }
        else
        {
            context.TransitionTo((int)EnemyAI.StateOptions.MoveToPlayer);
        }
    }

    IEnumerator TMPAttackAnimation()
    {
        renderer.material.color = Color.white;
        yield return new WaitForSecondsRealtime(animationTime);
        renderer.material.color = startColor;
    }
}

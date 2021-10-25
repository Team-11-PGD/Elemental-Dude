using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLavaSlamAttack : State
{
    [SerializeField]
    float chargeTime = 1.5f;
    [SerializeField]
    GameObject slamPrefab, lavaPrefab;
    [SerializeField]
    Transform slamPosition;
    [SerializeField]
    float damage = 0.1f;
    BossAI bossAI;

    public override void Enter()
    {
        bossAI = context as BossAI;
        StartCoroutine(ChargeTime());
    }

    public override void Exit() { }

    IEnumerator ChargeTime()
    {
        Instantiate(slamPrefab, slamPosition.position, context.transform.rotation, context.transform);
        yield return new WaitForSecondsRealtime(chargeTime);
        GameObject lavaInstance = Instantiate(lavaPrefab, slamPosition.position, context.transform.rotation);
        DamagingParticle damagingParticle = lavaInstance.GetComponentInChildren<DamagingParticle>();
        damagingParticle.damage = damage;
        damagingParticle.playerHealth = bossAI.playerHealth;

        lavaInstance.GetComponentInChildren<ParticleSystem>().trigger.AddCollider(bossAI.playerModel.GetComponent<Collider>());
        context.TransitionTo((int)BossAI.StateOptions.MoveToPlayer);
    }
}

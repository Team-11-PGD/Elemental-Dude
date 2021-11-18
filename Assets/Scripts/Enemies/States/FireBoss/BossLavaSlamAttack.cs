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
    FireBossAI bossAI;

    public override void Enter()
    {
        bossAI = context as FireBossAI;
        StartCoroutine(ChargeTime());
    }

    public override void Exit() { }

    IEnumerator ChargeTime()
    {
        Instantiate(slamPrefab, slamPosition.position, context.transform.rotation, context.transform);
        //SOUND: (slam)
        yield return new WaitForSecondsRealtime(chargeTime);
        //SOUND: (bubble bubble lava)
        GameObject lavaInstance = Instantiate(lavaPrefab, slamPosition.position, context.transform.rotation);
        DamagingParticle damagingParticle = lavaInstance.GetComponentInChildren<DamagingParticle>();
        damagingParticle.damage = damage;
        damagingParticle.playerHealth = bossAI.playerHealth;

        ParticleSystem particleSystemtmp = damagingParticle.GetComponent<ParticleSystem>();
        Collider collidertmp = bossAI.playerModel.GetComponent<Collider>();
        particleSystemtmp.trigger.AddCollider(collidertmp);

        if (!bossAI.SwitchToDefend()) bossAI.NextAttackState();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLavaSlamAttack : FireBossState
{
    public float lavaSize = 1;

    [SerializeField]
    float chargeTime = 1.5f;
    [SerializeField]
    GameObject slamPrefab, lavaPrefab;
    [SerializeField]
    Transform slamPosition;
    [SerializeField]
    float damage = 0.1f;

    public override void Enter(int previousStateId)
    {
        bossAI.animal.Mode_Activate(1,2);
        StartCoroutine(ChargeTime());
    }
    public override void Exit(int nextStateId) { }

    IEnumerator ChargeTime()
    {
        Instantiate(slamPrefab, slamPosition.position, context.transform.rotation, context.transform);
        //SOUND: (slam)
        yield return new WaitForSecondsRealtime(chargeTime);
        //SOUND: (bubble bubble lava)
        GameObject lavaInstance = Instantiate(lavaPrefab, slamPosition.position, context.transform.rotation);
        PlayerDamagingParticle damagingParticle = lavaInstance.GetComponentInChildren<PlayerDamagingParticle>();
        damagingParticle.damage = damage;
        damagingParticle.playerHealth = bossAI.playerHealth;
        damagingParticle.transform.localScale *= lavaSize;

        ParticleSystem particleSystemtmp = damagingParticle.GetComponent<ParticleSystem>();
        Collider playerModel = bossAI.playerModel.GetComponent<Collider>();
        particleSystemtmp.trigger.AddCollider(playerModel);
        bossAI.TransitionTo(FireBossAI.StateOptions.MoveToPlayer);
    }
}

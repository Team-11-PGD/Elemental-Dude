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
        StartCoroutine(ChargeTime());
    }
    public override void Exit(int nextStateId) { }

    IEnumerator ChargeTime()
    {
        Instantiate(slamPrefab, slamPosition.position, context.transform.rotation, context.transform);
        //SOUND: Check(slam)
        AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "BossSlamAttack");

        yield return new WaitForSecondsRealtime(chargeTime);
        //SOUND: Check(bubble bubble lava)
        
        GameObject lavaInstance = Instantiate(lavaPrefab, slamPosition.position, context.transform.rotation);
        DamagingParticle damagingParticle = lavaInstance.GetComponentInChildren<DamagingParticle>();
        damagingParticle.damage = damage;
        damagingParticle.playerHealth = bossAI.playerHealth;
        damagingParticle.transform.localScale *= lavaSize;
        AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "BossLavaAttack");

        ParticleSystem particleSystemtmp = damagingParticle.GetComponent<ParticleSystem>();
        Collider playerModel = bossAI.playerModel.GetComponent<Collider>();
        particleSystemtmp.trigger.AddCollider(playerModel);
        bossAI.TransitionTo(FireBossAI.StateOptions.MoveToPlayer);
    }
}

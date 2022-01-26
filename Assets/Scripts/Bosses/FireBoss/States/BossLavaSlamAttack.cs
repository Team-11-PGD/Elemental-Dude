using System.Collections;
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
        BossAI.animal.Mode_Activate(1,2);
        StartCoroutine(SlamCoroutine());
    }

    IEnumerator SlamCoroutine()
    {
        // Attack indication
        Instantiate(slamPrefab, slamPosition.position, context.transform.rotation, context.transform);
        AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "BossSlamAttack");

        yield return new WaitForSeconds(chargeTime);
        
        // Spawn lava
        GameObject lavaInstance = Instantiate(lavaPrefab, slamPosition.position, context.transform.rotation);
        PlayerDamagingParticle damagingParticle = lavaInstance.GetComponentInChildren<PlayerDamagingParticle>();
        damagingParticle.damage = damage;
        damagingParticle.playerHealth = BossAI.playerHealth;
        damagingParticle.transform.localScale *= lavaSize;
        AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "BossLavaAttack");

        ParticleSystem particleSystemtmp = damagingParticle.GetComponent<ParticleSystem>();
        Collider playerModel = BossAI.playerModel.GetComponent<Collider>();
        particleSystemtmp.trigger.AddCollider(playerModel);

        BossAI.NextState();
    }
}

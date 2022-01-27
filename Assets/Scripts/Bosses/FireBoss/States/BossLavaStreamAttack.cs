using System.Collections;
using UnityEngine;

public class BossLavaStreamAttack : FireBossState
{
    public int attackAmount = 1;

    [SerializeField]
    GameObject prefab;
    [SerializeField]
    Transform startPosition;

    [SerializeField]
    float timeBetweenAttacks = 3f;
    [SerializeField]
    float rotationTime = 1f;
    [SerializeField]
    float damage = 0.01f;

    public override void Enter(int previousStateId)
    {
        StartCoroutine(LavaStreamCoroutine());
    }

    public void Update()
    {
        Vector3 playerPosition = new Vector3(BossAI.playerModel.transform.position.x, transform.position.y, BossAI.playerModel.transform.position.z);
        transform.LookAt(playerPosition);
    }

    IEnumerator LavaStreamCoroutine()
    {
        yield return new WaitForSeconds(rotationTime);

        for (int i = 0; i < attackAmount; i++)
        {
            AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "BossLavaStreamAttack");

            GameObject groundShatterInstance = Instantiate(prefab, startPosition.position, context.transform.rotation, null);
            PlayerDamagingParticle damagingParticle = groundShatterInstance.GetComponentInChildren<PlayerDamagingParticle>();
            damagingParticle.damage = damage;
            damagingParticle.playerHealth = BossAI.playerHealth;

            ParticleSystem particleSystemtmp = damagingParticle.GetComponent<ParticleSystem>();
            Collider collidertmp = BossAI.playerModel.GetComponent<Collider>();
            particleSystemtmp.trigger.AddCollider(collidertmp);     
            
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        BossAI.NextState();
    }
}

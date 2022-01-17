using System.Collections;
using UnityEngine;

public class BossDefendingLavaStreamState : FireBossState
{
    public int instantiateAmount = 1;

    [SerializeField]
    GameObject groundbreakPrefab;
    [SerializeField]
    Transform groundbreakStartPosition;

    [SerializeField]
    float groundbreakTime = 3f;
    [SerializeField]
    float groundbreakDamage = 0.01f;

    public override void Enter(int previousStateId)
    {
        StartCoroutine(GroundbreakTimer());
    }

    public override void Exit(int nextStateId) { }

    public void Update()
    {
        Vector3 playerPosition = new Vector3(bossAI.playerModel.transform.position.x, transform.position.y, bossAI.playerModel.transform.position.z);
        transform.LookAt(playerPosition);
    }

    IEnumerator GroundbreakTimer()
    {
        for (int i = 0; i < instantiateAmount; i++)
        {
            GameObject groundbreakInstance = Instantiate(groundbreakPrefab, groundbreakStartPosition.position, context.transform.rotation, null);
            DamagingParticle damagingParticle = groundbreakInstance.GetComponentInChildren<DamagingParticle>();
            damagingParticle.damage = groundbreakDamage;
            damagingParticle.playerHealth = bossAI.playerHealth;

            ParticleSystem particleSystemtmp = damagingParticle.GetComponent<ParticleSystem>();
            Collider collidertmp = bossAI.playerModel.GetComponent<Collider>();
            particleSystemtmp.trigger.AddCollider(collidertmp);         
            yield return new WaitForSecondsRealtime(groundbreakTime);
        }
        bossAI.NextDefendState();
    }
}

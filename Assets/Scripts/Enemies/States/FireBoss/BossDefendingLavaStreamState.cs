using System.Collections;
using UnityEngine;

public class BossDefendingLavaStreamState : State
{
    [SerializeField]
    GameObject groundbreakPrefab;
    [SerializeField]
    Transform groundbreakStartPosition;

    [SerializeField]
    float groundbreakTime = 3f;
    [SerializeField]
    float groundbreakDamage = 0.01f;

    FireBossAI bossAI;

    public override void Enter(int previousStateId)
    {
        bossAI = context as FireBossAI;
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
        yield return new WaitForSecondsRealtime(groundbreakTime);
        GameObject groundbreakInstance = Instantiate(groundbreakPrefab, groundbreakStartPosition.position, context.transform.rotation, null);
        DamagingParticle damagingParticle = groundbreakInstance.GetComponentInChildren<DamagingParticle>();
        damagingParticle.damage = groundbreakDamage;
        damagingParticle.playerHealth = bossAI.playerHealth;

        ParticleSystem particleSystemtmp = damagingParticle.GetComponent<ParticleSystem>();
        Collider collidertmp = bossAI.playerModel.GetComponent<Collider>();
        particleSystemtmp.trigger.AddCollider(collidertmp);
        bossAI.NextDefendState();
    }
}

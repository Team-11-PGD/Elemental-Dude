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
    float groundbreakDuration = 3;
    [SerializeField]
    float lookAtTime = 1;
    [SerializeField]
    float groundbreakDamage = 1;

    public override void Enter(int previousStateId)
    {
        StartCoroutine(GroundbreakTimer());
    }

    public override void Exit(int nextStateId) { }

    public void Update()
    {
        //make the boss look at the player
        Vector3 playerPosition = new Vector3(bossAI.playerModel.position.x, transform.position.y, bossAI.playerModel.position.z);
        transform.LookAt(playerPosition);
    }

    IEnumerator GroundbreakTimer()
    {
        yield return new WaitForSecondsRealtime(lookAtTime);
        for (int i = 0; i < instantiateAmount; i++)
        {
            AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "BossLavaStreamAttack");

            //instantiate groundbrake prefab and set the damaging particles from this prefab to do damage equal to groundbreakdamage
            GameObject groundbreakInstance = Instantiate(groundbreakPrefab, groundbreakStartPosition.position, context.transform.rotation);
            PlayerDamagingParticle damagingParticle = groundbreakInstance.GetComponentInChildren<PlayerDamagingParticle>();
            damagingParticle.damage = groundbreakDamage;

            //the particles from groundbrakeinstance become damageing particles and make them collide with the player wich results in a trigger
            //ParticleSystem particleSystemtmp = damagingParticle.GetComponent<ParticleSystem>();
            Collider collidertmp = bossAI.playerModel.GetComponent<Collider>();
            damagingParticle.GetComponent<ParticleSystem>().trigger.AddCollider(collidertmp);
            yield return new WaitForSecondsRealtime(groundbreakDuration);
            
        }
        bossAI.NextDefendState();
    }
}

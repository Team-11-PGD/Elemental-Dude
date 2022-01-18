using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlameBreathAttack : FireBossState
{
    public float size = 1;

    new GameObject particleSystem;
    [SerializeField]
    Transform FlamePosition;

    [SerializeField]
    GameObject smokePrefab, flamePrefab;
    [SerializeField]
    float smokeTime = 0.5f, attackTime = 2f;
    [SerializeField]
    float damage = 0.01f;

    bool facePlayer;

    public override void Enter(int previousStateId)
    {
        facePlayer = true;

        StartCoroutine(SmokeTimer());
        //SOUND: Check(Flamethrower)
        AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "BossFlamethrowerAttack");
    }

    public override void Exit(int nextStateId) 
    {
        StopAllCoroutines();
        particleSystem?.GetComponent<ParticleRemover>().ShutDown();
    }

    void Update()
    {
        if (!facePlayer) return;

        Vector3 playerPosition = new Vector3(bossAI.playerModel.transform.position.x, transform.position.y, bossAI.playerModel.transform.position.z);
        transform.LookAt(playerPosition);
    }

    IEnumerator SmokeTimer()
    {
        particleSystem = Instantiate(smokePrefab, FlamePosition.transform.position, context.transform.rotation, context.transform);
        yield return new WaitForSecondsRealtime(smokeTime);

        facePlayer = false;

        particleSystem.GetComponent<ParticleRemover>().ShutDown();

        particleSystem = Instantiate(flamePrefab, FlamePosition.transform.position, context.transform.rotation, context.transform);
        particleSystem.GetComponent<DamagingParticle>().playerHealth = bossAI.playerHealth;
        particleSystem.GetComponent<DamagingParticle>().damage = this.damage;
        particleSystem.transform.localScale *= size;
        StartCoroutine(FlameTimer());
    }

    IEnumerator FlameTimer()
    { 
        yield return new WaitForSecondsRealtime(attackTime);
        particleSystem.GetComponent<ParticleRemover>().ShutDown();

        bossAI.TransitionTo(FireBossAI.StateOptions.MoveToPlayer);
    }
}

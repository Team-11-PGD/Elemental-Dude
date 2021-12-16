using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlameBreathAttack : State
{
    new GameObject particleSystem;
    [SerializeField]
    Transform FlamePosition;

    [SerializeField]
    GameObject smokePrefab, flamePrefab;
    [SerializeField]
    float smokeTime = 0.5f, attackTime = 2f;
    [SerializeField]
    float damage = 0.01f;

    FireBossAI bossAI;
    bool facePlayer;

    public override void Enter(int previousStateId)
    {
        bossAI = context as FireBossAI;

        facePlayer = true;

        StartCoroutine(SmokeTimer());
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
        //SOUND: (smoky)
        particleSystem = Instantiate(smokePrefab, FlamePosition.transform.position, context.transform.rotation, context.transform);
        yield return new WaitForSecondsRealtime(smokeTime);

        facePlayer = false;

        particleSystem.GetComponent<ParticleRemover>().ShutDown();

        particleSystem = Instantiate(flamePrefab, FlamePosition.transform.position, context.transform.rotation, context.transform);
        particleSystem.GetComponent<DamagingParticle>().playerHealth = bossAI.playerHealth;
        particleSystem.GetComponent<DamagingParticle>().damage = this.damage;
        StartCoroutine(FlameTimer());
    }

    IEnumerator FlameTimer()
    {
        //SOUND: (flame)
        yield return new WaitForSecondsRealtime(attackTime);
        particleSystem.GetComponent<ParticleRemover>().ShutDown();

        bossAI.TransitionTo(FireBossAI.StateOptions.MoveToPlayer);
    }
}

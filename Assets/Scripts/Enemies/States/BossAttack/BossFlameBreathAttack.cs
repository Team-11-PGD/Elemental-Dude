using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlameBreathAttack : State
{
    new GameObject particleSystem;

    [SerializeField]
    GameObject smokePrefab, flamePrefab;
    [SerializeField]
    float smokeTime = 0.5f, attackTime = 2f;
    [SerializeField]
    float damage = 0.01f;

    public enum bossAttacks
    {
        fireBreath,
        fireSlam,
        fireballRain,
        lavaStream
    }
    public bossAttacks currentState;

    public override void Enter()
    {
        particleSystem = Instantiate(smokePrefab, context.transform.position, context.transform.rotation, context.transform);
        StartCoroutine(SmokeTimer());
    }

    public override void Exit() { }

    IEnumerator SmokeTimer()
    {
        yield return new WaitForSecondsRealtime(smokeTime);
        particleSystem.GetComponent<ParticleRemover>().ShutDown();

        particleSystem = Instantiate(flamePrefab, context.transform.position, context.transform.rotation, context.transform);
        particleSystem.GetComponent<DamagingParticle>().damage = this.damage;
        StartCoroutine(FlameTimer());
    }

    IEnumerator FlameTimer()
    {
        yield return new WaitForSecondsRealtime(attackTime);
        particleSystem.GetComponent<ParticleRemover>().ShutDown();
        context.TransitionTo((int)BossAI.StateOptions.MoveToPlayer);
    }


}

using System.Collections;
using UnityEngine;

public class BossFlameBreathAttack : FireBossState
{
    public float size = 1;

    new GameObject particleSystem;
    [SerializeField]
    Transform flamePosition;

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

        StartCoroutine(SmokeCoroutine());
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

        Vector3 playerPosition = BossAI.playerModel.transform.position;
        playerPosition.y = transform.position.y;
        transform.LookAt(playerPosition);
    }

    IEnumerator SmokeCoroutine()
    {
        particleSystem = Instantiate(smokePrefab, flamePosition.transform.position, context.transform.rotation, context.transform);

        yield return new WaitForSeconds(smokeTime);

        facePlayer = false;
        particleSystem.GetComponent<ParticleRemover>().ShutDown();

        StartCoroutine(FlameCoroutine());
    }

    IEnumerator FlameCoroutine()
    {
        particleSystem = Instantiate(flamePrefab, flamePosition.transform.position, flamePosition.transform.rotation, context.transform);
        particleSystem.GetComponent<PlayerDamagingParticle>().playerHealth = BossAI.playerHealth;
        particleSystem.GetComponent<PlayerDamagingParticle>().damage = damage;
        particleSystem.transform.localScale *= size;

        yield return new WaitForSeconds(attackTime);

        particleSystem.GetComponent<ParticleRemover>().ShutDown();

        BossAI.NextState();
    }
}

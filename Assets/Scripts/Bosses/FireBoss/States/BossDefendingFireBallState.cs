using System.Collections;
using UnityEngine;

// Joshua Knaven
public class BossDefendingFireBallState : FireBossState
{
    public int sizeFireball = 1;
    public float percentageOfRoomFilled = 0.1f;

    [SerializeField]
    BoxCollider spawnArea;
    [SerializeField]
    LayerMask groundLayer;

    [Header("Attacking")]
    [SerializeField]
    GameObject fireball;
    [SerializeField]
    int fireballAmount = 10;
    [SerializeField]
    float spawningTime = 2;
    [SerializeField]
    float damage = 1;

    private float amountOFireballs;
    Vector3[] spawningPositions;

    public override void Enter(int previousStateId)
    {
        StartCoroutine(SpawnAttack());
        float sizeSpawnArea = spawnArea.bounds.size.x * spawnArea.bounds.size.z;
        amountOFireballs = (sizeSpawnArea / sizeFireball) * percentageOfRoomFilled;
        Debug.Log(amountOFireballs);
        Debug.Log(sizeSpawnArea + "area");
        Debug.Log(percentageOfRoomFilled + "prec");
    }

    public override void Exit(int nextStateId) { }

    public void Update()
    {
        context.transform.Rotate(Vector3.up, 2);
    }

    IEnumerator SpawnAttack()
    {
        spawningPositions = new Vector3[fireballAmount];
        for (int i = 0; i < spawningPositions.Length; i++)
        {
            spawningPositions[i] = new Vector3(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

            if (Physics.Raycast(new Ray(spawningPositions[i], Vector3.down), out RaycastHit hit, float.MaxValue, groundLayer))
            {
                spawningPositions[i] = hit.point;
            }

            GameObject instance = Instantiate(fireball, spawningPositions[i], Quaternion.Euler(-90, 0, 0));
            PlayerDamagingParticle playerDamagingParticle = instance.GetComponentInChildren<PlayerDamagingParticle>();
            playerDamagingParticle.damage = damage;
            playerDamagingParticle.playerHealth = bossAI.playerHealth;
            playerDamagingParticle.GetComponent<ParticleSystem>().trigger.AddCollider(bossAI.playerModel.GetComponent<Collider>());
        }

        yield return new WaitForSecondsRealtime(spawningTime);
        bossAI.NextDefendState();
    }
}

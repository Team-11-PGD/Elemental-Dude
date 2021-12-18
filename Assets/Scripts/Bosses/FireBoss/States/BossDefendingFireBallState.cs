using System.Collections;
using UnityEngine;

// Joshua Knaven
public class BossDefendingFireBallState : FireBossState
{
    [SerializeField]
    BoxCollider spawnArea;

    [Header("Indication")]
    [SerializeField]
    float shockWaveSize = 13f;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    GameObject shockWaveIndicator;
    float indicationTime = 2;

    [Header("Attacking")]
    [SerializeField]
    GameObject fireball;
    [SerializeField]
    int fireballAmount = 10;
    [SerializeField]
    float spawningTime = 2;
    [SerializeField]
    float damage = 1;

    Vector3[] spawningPositions;

    public override void Enter(int previousStateId)
    {
        StartCoroutine(AnounceFireballs());
    }

    public override void Exit(int nextStateId) { }

    public void Update()
    {
        context.transform.Rotate(Vector3.up, 2);
    }

    IEnumerator AnounceFireballs()
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
                GameObject instance = Instantiate(shockWaveIndicator, hit.point, Quaternion.identity);
                instance.GetComponent<GameObjectRemover>().ShutDown(indicationTime);
                instance.transform.localScale *= shockWaveSize;
            }
        }

        yield return new WaitForSecondsRealtime(indicationTime);

        StartCoroutine(SpawnFireballs());
    }

    IEnumerator SpawnFireballs()
    {
        for (int i = 0; i < spawningPositions.Length; i++)
        {
            GameObject instance = Instantiate(fireball, spawningPositions[i], Quaternion.identity);
            instance.GetComponent<Fireball>().SetupParticleDamage(bossAI.playerHealth, bossAI.playerModel.GetComponent<Collider>(), damage);
        }
        yield return new WaitForSecondsRealtime(spawningTime);
        bossAI.NextDefendState();
    }
}

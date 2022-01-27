using System.Collections;
using UnityEngine;

// Joshua Knaven
public class BossOrbitalBeamAttack : FireBossState
{
    public int orbitalBeamAmount = 10;

    [SerializeField]
    BoxCollider spawnArea;
    [SerializeField]
    LayerMask groundLayer;

    [Header("Attacking")]
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    float spawningTime = 2;
    [SerializeField]
    float damage = 1;

    Vector3[] spawningPositions;

    public override void Enter(int previousStateId)
    {
        StartCoroutine(BeamCoroutine());
    }

    public void Update()
    {
        context.transform.Rotate(Vector3.up, 2);
    }

    IEnumerator BeamCoroutine()
    {
        spawningPositions = new Vector3[orbitalBeamAmount];

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

            GameObject instance = Instantiate(prefab, spawningPositions[i], Quaternion.Euler(-90, 0, 0));
            PlayerDamagingParticle playerDamagingParticle = instance.GetComponentInChildren<PlayerDamagingParticle>();
            playerDamagingParticle.damage = damage;
            playerDamagingParticle.playerHealth = BossAI.playerHealth;
            playerDamagingParticle.GetComponent<ParticleSystem>().trigger.AddCollider(BossAI.playerModel.GetComponent<Collider>());
        }

        yield return new WaitForSeconds(spawningTime);
        BossAI.NextState();
    }
}

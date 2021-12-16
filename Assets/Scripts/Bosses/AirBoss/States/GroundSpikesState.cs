using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpikesState : State
{
    [SerializeField]
    Collider spawnArea;
    [SerializeField]
    GameObject spike;
    [SerializeField]
    int spikeCount = 20;
    [SerializeField]
    float spawningTime = 2f;

    AirBossAI bossAI;

    public override void Enter(int previousStateId)
    {
        bossAI = context as AirBossAI;
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        Spike[] spikes = new Spike[spikeCount];
        for (int i = 0; i < spikeCount; i++)
        {
            spikes[i] = Instantiate(spike, GetPointInCollider(), Quaternion.identity).GetComponent<Spike>();
            spikes[i].playerHealth = bossAI.playerHealth;
        }

        yield return new WaitForSecondsRealtime(spawningTime);

        for (int i = 0; i < spikeCount; i++)
        {
            spikes[i].enabled = true;
        }
    }

    Vector3 GetPointInCollider()
    {
        Vector3 point = new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

        Vector3 tmp = spawnArea.ClosestPoint(point);
        return tmp;
    }
}

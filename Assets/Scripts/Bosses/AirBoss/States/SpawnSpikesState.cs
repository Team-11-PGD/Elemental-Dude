using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joshua Knaven
public class SpawnSpikesState : AirBossState
{
    [Range(0, 1)]
    public float spikePercentage = 0.25f;
    public bool isGroundSpike;

    [SerializeField]
    Collider spawnArea;
    [SerializeField]
    GameObject spike;
    [SerializeField]
    float spikeSize = 1;
    [SerializeField]
    float spawningTime = 2f;


    public override void Enter(int previousStateId)
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        float area = spawnArea.bounds.size.x * spawnArea.bounds.size.z;
        float maxSpikes = area / spikeSize;
        int spikeCount = (int)(maxSpikes * spikePercentage);

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
            spikes[i].isGroundSpike = isGroundSpike;
        }

        //context.NextRandomState(true, AirBossAI.StateOptions.Tornado);
        context.NextRandomState(bossAI.CurrentStateOptions);
    }

    Vector3 GetPointInCollider()
    {
        Vector3 point = new Vector3(
            UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            UnityEngine.Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
            UnityEngine.Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

        Vector3 tmp = spawnArea.ClosestPoint(point);
        return tmp;
    }
}

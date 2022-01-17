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
            Vector3 point = new Vector3(bossAI.playerModel.position.x, bossAI.playerModel.position.y - 1, bossAI.playerModel.position.z);
            spikes[i] = Instantiate(spike, point, Quaternion.identity).GetComponent<Spike>();
            spikes[i].playerHealth = bossAI.playerHealth;
            yield return new WaitForSecondsRealtime(spawningTime);
            spikes[i].enabled = true;
            spikes[i].isGroundSpike = isGroundSpike;            
        }
        //context.NextRandomState(true, AirBossAI.StateOptions.Tornado);
        context.NextRandomState(bossAI.CurrentStateOptions);
    }
}

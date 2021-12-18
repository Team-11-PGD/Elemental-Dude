using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableState : State
{
    [SerializeField]
    int healthPickupAmount = 3;
    [SerializeField]
    GameObject fallingHealthPickup;
    [SerializeField]
    BoxCollider healthSpawnArea;
    [SerializeField]
    GameObject uiObjectHealthPickup;

    List<GameObject> healthPickups;

    public override void Enter(int previousStateId)
    {
        healthPickups = new List<GameObject>();
        for (int i = 0; i < healthPickupAmount; i++)
        {
            Vector3 spawningPosition = new Vector3(
            Random.Range(healthSpawnArea.bounds.min.x, healthSpawnArea.bounds.max.x),
            Random.Range(healthSpawnArea.bounds.min.y, healthSpawnArea.bounds.max.y),
            Random.Range(healthSpawnArea.bounds.min.z, healthSpawnArea.bounds.max.z));

            healthPickups.Add(Instantiate(fallingHealthPickup, spawningPosition, Quaternion.identity));
            healthPickups[healthPickups.Count - 1].GetComponent<ShowPickupText>().uiObject = uiObjectHealthPickup;
        }
    }

    public override void Exit(int nextStateId)
    {
        for (int i = 0; i < healthPickups.Count; i++)
        {
            if (healthPickups[i] != null) Destroy(healthPickups[i]);
        }
    }
}

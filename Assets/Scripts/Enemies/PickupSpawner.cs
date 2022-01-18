using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Serializable]
    struct PickupOptions
    {
        public GameObject pickup;
        [Range(0, 1)]
        public float spawnChance;
    }

    [SerializeField] GameObject uiObject;
    [SerializeField] Health health;
    [SerializeField] PickupOptions[] pickupOptions;

    void Awake()
    {
        health ??= GetComponent<Health>();
    }

    void Start()
    {
        health.Died += DropPickup;

        float totalChance = 0;
        foreach (PickupOptions pickupOption in pickupOptions) totalChance += pickupOption.spawnChance;

        if (totalChance != 1)
        {
            for (int i = 0; i < pickupOptions.Length; i++)
            {
                pickupOptions[i].spawnChance *= 1 / totalChance;
            }
        }
    }

    void OnDisable()
    {
        health.Died -= DropPickup;
    }

    void DropPickup()
    {
        float chance = UnityEngine.Random.value;
        int i = 0;

        do
        {
            if (chance >= pickupOptions[i].spawnChance)
            {
                ShowPickupText showPickupText = Instantiate(pickupOptions[i].pickup, transform.position, Quaternion.identity, null).GetComponent<ShowPickupText>();
                showPickupText.uiObject = uiObject;
                break;
            }
            i++;
        } while (chance < pickupOptions[i].spawnChance);
    }
}

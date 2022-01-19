using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    [SerializeField]
    public Transform location;
    [SerializeField]
    public Health health;
    [SerializeField]
    private GameObject[] pickups;
    [SerializeField] GameObject uiObject;

    public int lowestRandom = 1;
    public int highestRandom = 8;
    private int randomValue;

    private void Start()
    {
        health.Died += ItemDrop;
        randomValue = UnityEngine.Random.Range(lowestRandom, highestRandom);
    }

    private void ItemDrop()
    {
        if (randomValue == 1)//damage
        {
            ShowPickupText showPickupText = Instantiate(pickups[0], location.position, location.rotation).GetComponent<ShowPickupText>();
            showPickupText.uiObject = uiObject;
        }
        if (randomValue == 2 || randomValue == 3)//health
        {
            ShowPickupText showPickupText = Instantiate(pickups[1], location.position, location.rotation).GetComponent<ShowPickupText>();
            showPickupText.uiObject = uiObject;
        }
        if (randomValue == 4)//speed
        {
            ShowPickupText showPickupText = Instantiate(pickups[2], location.position, location.rotation).GetComponent<ShowPickupText>();
            showPickupText.uiObject = uiObject;
        }
    }
}
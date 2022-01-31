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

    public int lowestRandom = 1, highestRandom = 8;//values for the randomValue
    private int randomValue;

    private void Start()
    {
        health.Died += ItemDrop;
        randomValue = UnityEngine.Random.Range(lowestRandom, highestRandom);//generates a random number for the spawnrate
    }

    private void ItemDrop()
    {
        ItemSelect(1,0);
        ItemSelect(2,1,3);
        ItemSelect(4, 2);
    }
    private void ItemSelect(int randomValue,int pickupIndex,int randomValue1 = 0) 
    {
        if (this.randomValue == randomValue || this.randomValue == randomValue1)
        {
            ShowPickupText showPickupText = Instantiate(pickups[pickupIndex], location.position, location.rotation).GetComponent<ShowPickupText>();
            showPickupText.uiObject = uiObject;
        }
    }
}

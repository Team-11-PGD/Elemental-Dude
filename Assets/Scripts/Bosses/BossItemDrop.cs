using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossItemDrop : MonoBehaviour
{
    [SerializeField]
    public Transform location;
    [SerializeField]
    public Health health;
    [SerializeField]
    private GameObject[] pickups;
    [SerializeField] GameObject uiObject;
    [SerializeField] ElementMain element;

    private void Start()
    {
        health.Died += ItemDrop;
    }

    private void ItemDrop()
    {
        if (element.currentType == ElementMain.ElementType.Fire)//Fire
        {
            ShowPickupText showPickupText = Instantiate(pickups[0], location.position, location.rotation).GetComponent<ShowPickupText>();
            showPickupText.uiObject = uiObject;
        }
        if (element.currentType == ElementMain.ElementType.Water)//Water
        {
            ShowPickupText showPickupText = Instantiate(pickups[1], location.position, location.rotation).GetComponent<ShowPickupText>();
            showPickupText.uiObject = uiObject;
        }
        if (element.currentType == ElementMain.ElementType.Air)//Air
        {
            ShowPickupText showPickupText = Instantiate(pickups[2], location.position, location.rotation).GetComponent<ShowPickupText>();
            showPickupText.uiObject = uiObject;
        }
    }
}

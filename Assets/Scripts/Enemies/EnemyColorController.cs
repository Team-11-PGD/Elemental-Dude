using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorController : MonoBehaviour
{
    [SerializeField]
    ElementMain elementMain;
    [SerializeField]
    MeshFilter meshFilter;

    [SerializeField]
    Mesh water, fire, air, earth;

    void Start()
    {
        elementMain.currentType = RoomGeneration.CurrentElements[Random.Range(0, RoomGeneration.CurrentElements.Count)];

        //Water element
        if (elementMain.currentType == ElementMain.ElementType.Water)
        {
            meshFilter.mesh = water;
        }
        //Fire element
        if (elementMain.currentType == ElementMain.ElementType.Fire)
        {
            meshFilter.mesh = fire;
        }
        //Air element
        if (elementMain.currentType == ElementMain.ElementType.Air)
        {
            meshFilter.mesh = air;
        }
        //Earth element
        //if (elementMain.currentType == ElementMain.ElementType.Earth)
        //{
        //   meshFilter.mesh = earth;
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalColorPicker : MonoBehaviour
{
    [SerializeField]
    CrystalMeshList crystals;

    [HideInInspector]
    public ElementMain.ElementType element;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        string[] split = meshFilter.sharedMesh.name.Split('_');
        int index = int.Parse(split[split.Length - 1]) - 1;

        int rand = Random.Range(0, RoomGeneration.CurrentElements.Count);
        element = RoomGeneration.CurrentElements[rand];
        switch (RoomGeneration.CurrentElements[rand])
        {
            case ElementMain.ElementType.Water:
                meshFilter.mesh = crystals.waterCrystals[index];
                break;
            case ElementMain.ElementType.Fire:
                meshFilter.mesh = crystals.fireCrystals[index];
                break;
            case ElementMain.ElementType.Air:
                meshFilter.mesh = crystals.airCrystals[index];
                break;
            //case ElementMain.ElementType.Earth:
            //    meshFilter.mesh = crystals.earthCrystals[index];
            //    break;
        }
    }
}

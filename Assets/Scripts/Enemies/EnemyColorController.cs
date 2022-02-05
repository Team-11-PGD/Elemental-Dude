using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementMain;

public class EnemyColorController : MonoBehaviour
{
    [SerializeField]
    ElementMain elementMain;
    [SerializeField]
    MeshFilter meshFilter;
    Renderer slimeRender;

    void Start()
    {
        elementMain.currentType = RoomGeneration.CurrentElements[Random.Range(0, RoomGeneration.CurrentElements.Count)];
        if (elementMain.enemyType == EnemyType.Slime) slimeRender = GetComponent<Renderer>();
        EnemyColor();
    }

    private bool TypeRequirement(EnemyType enemyType, ElementType elementType)
        => elementMain.enemyType == enemyType && elementMain.currentType == elementType;


    private void EnemyColor()
    {
        if (TypeRequirement(EnemyType.Spiker, ElementType.Water))
            meshFilter.mesh = ElementColors.instance.waterMesh;

        if (TypeRequirement(EnemyType.Spiker, ElementType.Fire))
            meshFilter.mesh = ElementColors.instance.fireMesh;

        if (TypeRequirement(EnemyType.Spiker, ElementType.Air))
            meshFilter.mesh = ElementColors.instance.airMesh;

        if (TypeRequirement(EnemyType.Slime, ElementType.Water))
            slimeRender.material = ElementColors.instance.WaterSlime;

        if (TypeRequirement(EnemyType.Slime, ElementType.Fire))
            slimeRender.material = ElementColors.instance.FireSlime;

        if (TypeRequirement(EnemyType.Slime, ElementType.Air))
            slimeRender.material = ElementColors.instance.AirSlime;
    }
}

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

    private int randomValue;
    void Start()
    {
        randomValue = Random.Range(0, 3);
        elementMain.currentType = RoomGeneration.CurrentElements[Random.Range(0, RoomGeneration.CurrentElements.Count)];
        if (elementMain.enemyType == EnemyType.Slime) SlimeRandom();
        EnemyColor();
    }

    private bool TypeRequirement(EnemyType enemyType, ElementType elementType)
        => elementMain.enemyType == enemyType && elementMain.currentType == elementType;


    public void EnemyColor()
    {
        if (TypeRequirement(EnemyType.Spiker, ElementType.Water))
            meshFilter.mesh = ElementColors.instance.waterMesh;

        else if (TypeRequirement(EnemyType.Spiker, ElementType.Fire))
            meshFilter.mesh = ElementColors.instance.fireMesh;

        else if (TypeRequirement(EnemyType.Spiker, ElementType.Air))
            meshFilter.mesh = ElementColors.instance.airMesh;

        else if (TypeRequirement(EnemyType.Slime, ElementType.Water))
            slimeRender.material = ElementColors.instance.WaterSlime;

        else if (TypeRequirement(EnemyType.Slime, ElementType.Fire))
            slimeRender.material = ElementColors.instance.FireSlime;

        else if (TypeRequirement(EnemyType.Slime, ElementType.Air))
            slimeRender.material = ElementColors.instance.AirSlime;
    }
    public void SlimeRandom()
    {
        slimeRender = GetComponent<Renderer>();
        if (randomValue == 0) elementMain.currentType = ElementType.Air;
        else if (randomValue == 1) elementMain.currentType = ElementType.Water;
        else if (randomValue == 2) elementMain.currentType = ElementType.Fire;
    }
}

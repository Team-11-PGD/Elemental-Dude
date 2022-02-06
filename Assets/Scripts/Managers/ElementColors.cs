using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementColors : MonoBehaviour
{
    public static ElementColors instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField]
    public Mesh waterMesh, fireMesh, airMesh;//meshes for the spikers

    public static readonly Color NoneColor = Color.white;
    public static readonly Color WaterColor = new Color(4f / 256, 254f / 256, 239f / 256);
    public static readonly Color FireColor = new Color(248f / 256, 49f / 256, 86f / 256);
    public static readonly Color AirColor = new Color(254f / 256, 226f / 256, 82f / 256);

    public Material FireSlime, WaterSlime, AirSlime;//material for the slimes

    public Material FireMaterial;
    public Material WaterMaterial;
    public Material AirMaterial;

    public static Material GetElement(ElementMain.ElementType elementType) => elementType switch
    {
        ElementMain.ElementType.Water => instance.WaterMaterial,
        ElementMain.ElementType.Fire => instance.FireMaterial,
        ElementMain.ElementType.Air => instance.AirMaterial,
        //ElementMain.ElementType.Earth => instance.EarthMaterial,
        _ => throw new System.NotImplementedException()
    };
}


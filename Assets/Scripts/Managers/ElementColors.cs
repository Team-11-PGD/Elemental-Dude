using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementColors : MonoBehaviour
{
    public static ElementColors instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.parent = null;
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public static readonly Color NoneColor = Color.white;
    public static readonly Color WaterColor = new Color(4, 254, 239);
    public static readonly Color FireColor = new Color(248, 49, 86);
    public static readonly Color AirColor = new Color(254, 226, 82);
    public static readonly Color EarthColor = new Color(59, 254, 90);

    public Material FireMaterial;
    public Material WaterMaterial;
    public Material AirMaterial;
    public Material EarthMaterial;

    public static Material GetElement(ElementMain.ElementType elementType) => elementType switch
    {
        ElementMain.ElementType.Water => instance.WaterMaterial,
        ElementMain.ElementType.Fire => instance.FireMaterial,
        ElementMain.ElementType.Air => instance.AirMaterial,
        ElementMain.ElementType.Earth => instance.EarthMaterial,
        _ => throw new System.NotImplementedException()
    };
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaster : MonoBehaviour
{
    public static ColorMaster instance;

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

    public Material ElementFire;
    public Material ElementWater;
    public Material ElementAir;
    public Material ElementEarth;

    public static Material GetElement(ElementMain.ElementType elementType) => elementType switch
    {
        ElementMain.ElementType.Water => instance.ElementWater,
        ElementMain.ElementType.Fire => instance.ElementFire,
        ElementMain.ElementType.Air => instance.ElementAir,
        ElementMain.ElementType.Earth => instance.ElementEarth,
        _ => throw new System.NotImplementedException()
    };
}


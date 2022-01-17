using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementLight : MonoBehaviour
{
    [SerializeField]
    Light light;
    [SerializeField]
    CrystalColorPicker crystal;

    // Start is called before the first frame update
    void Start()
    {
        switch (crystal.element)
        {
            case ElementMain.ElementType.None:
                light.color = ElementColors.NoneColor;
                break;
            case ElementMain.ElementType.Water:
                light.color = ElementColors.WaterColor;
                break;
            case ElementMain.ElementType.Fire:
                light.color = ElementColors.FireColor;
                break;
            case ElementMain.ElementType.Air:
                light.color = ElementColors.AirColor;
                break;
            //case ElementMain.ElementType.Earth:
            //    light.color = ElementColors.EarthColor;
            //    break;
            default:
                break;
        }
    }
}

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
                break;
            case ElementMain.ElementType.Fire:
                break;
            case ElementMain.ElementType.Air:
                break;
            case ElementMain.ElementType.Earth:
                break;
            default:
                break;
        }
    }
}

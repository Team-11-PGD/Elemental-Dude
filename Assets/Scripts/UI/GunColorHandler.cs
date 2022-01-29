using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunColorHandler : MonoBehaviour
{
    [SerializeField]
    ElementMain element;

    [SerializeField]
    private Image Selector, GunElement;

    [SerializeField]
    WeaponUI weapon;

    public void SetColor() 
    {
        switch (element.currentType)
        {
            case ElementMain.ElementType.None:
                Selector.color = Color.white;
                GunElement.enabled = false;
                break;
            case ElementMain.ElementType.Water:
                Selector.color = ElementColors.WaterColor;
                ElementEnable();
                GunElement.sprite = weapon.waterSprite;
                break;
            case ElementMain.ElementType.Fire:
                Selector.color = ElementColors.FireColor;
                ElementEnable();
                GunElement.sprite = weapon.fireSprite;
                break;
            case ElementMain.ElementType.Air:
                Selector.color = ElementColors.AirColor;
                ElementEnable();
                GunElement.sprite = weapon.airSprite;
                break;
            default:
                break;
        }
    }
    private void ElementEnable() 
    {
        GunElement.enabled = true;
    }
}

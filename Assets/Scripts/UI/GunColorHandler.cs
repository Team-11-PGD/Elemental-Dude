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
        switch (element.currentType)//switch case for the color of the weapon selector.
        {
            case ElementMain.ElementType.None://no element is applied
                SwapColor(ElementColors.NoneColor, elementEnabled: false);
                break;

            case ElementMain.ElementType.Water://water element is applied
                SwapColor(ElementColors.WaterColor, weapon.waterSprite);
                break;

            case ElementMain.ElementType.Fire://fire element is applied
                SwapColor(ElementColors.FireColor, weapon.fireSprite);
                break;

            case ElementMain.ElementType.Air://air element is applied
                SwapColor(ElementColors.AirColor, weapon.airSprite);
                break;

            default:
                break;
        }
    }
    private void SwapColor(Color color, Sprite sprite = null, bool elementEnabled = true)
    {
        Selector.color = color;
        GunElement.enabled = elementEnabled;
        if (sprite != null) GunElement.sprite = sprite;
    }
}

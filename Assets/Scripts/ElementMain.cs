using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElementMain : MonoBehaviour
{
    [HideInInspector]
    public float advantageElemPercentage = 1.5f;
    [HideInInspector]
    public float disadvantageElemPercentage = 0.5f;
    public ElementType currentType = ElementType.None;

    public static Color ElementNone = Color.white;
    public static Color ElementWater = Color.blue;
    public static Color ElementFire = new Color(1, 0.4f, 0);
    public static Color ElementAir = Color.grey;
    public static Color ElementEarth = new Color(0.59f, 0.38f, 0.21f);

    public enum ElementType
    {
        None,
        Water,
        Fire,
        Air,
        Earth
    }

    public float ElementDmgPercentage(ElementType thisElement, ElementType otherElement)
    {
        switch (thisElement)
        {
            case ElementType.Water:
                if (otherElement == ElementType.Fire)
                {
                    return advantageElemPercentage;
                }
                else if (otherElement == ElementType.Earth)
                {
                    return disadvantageElemPercentage;
                }
                break;

            case ElementType.Fire:
                if (otherElement == ElementType.Air)
                {
                    return advantageElemPercentage;
                }
                else if (otherElement == ElementType.Water)
                {
                    return disadvantageElemPercentage;
                }
                break;

            case ElementType.Air:
                if (otherElement == ElementType.Earth)
                {
                    return advantageElemPercentage;
                }
                else if (otherElement == ElementType.Fire)
                {
                    return disadvantageElemPercentage;
                }
                break;

            case ElementType.Earth:
                if (otherElement == ElementType.Water)
                {
                    return advantageElemPercentage;
                }
                else if (otherElement == ElementType.Air)
                {
                    return disadvantageElemPercentage;
                }
                break;
        }
        return 1;
    }
}

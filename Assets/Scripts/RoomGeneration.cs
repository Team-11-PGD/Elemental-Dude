using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomGeneration
{
    enum ElementType
    {
        Water,
        Fire,
        Air,
        Earth
    }

    ElementType type;

    private Dictionary<ElementType, bool> UsedElements = new Dictionary<ElementType, bool>
    { 
        { ElementType.Water, true}
    };

    public void LoadNextRoom()
    {
        int rand = UnityEngine.Random.Range(0, (int)Enum.GetValues(typeof(ElementType)).Cast<ElementType>().Max());

        type = (ElementType)rand;
    }
}

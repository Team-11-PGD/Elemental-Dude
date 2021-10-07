using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static public class RoomGeneration
{
    /// <summary>
    /// A list of all active elements in the level
    /// </summary>
    public static List<ElementMain.ElementType> CurrentElements { get; private set; } = new List<ElementMain.ElementType>();

    static List<ElementMain.ElementType> availableElements = new List<ElementMain.ElementType>();

    /// <summary>
    /// Reset element list
    /// </summary>
    static void RefillAvailableElements()
    {
        availableElements = new List<ElementMain.ElementType>
        {
             ElementMain.ElementType.Water,
             ElementMain.ElementType.Fire,
             ElementMain.ElementType.Air,
             ElementMain.ElementType.Earth
        };
    }

    /// <summary>
    /// Load next room with new random elements
    /// </summary>
    /// <param name="amountOfElements"> Amount of elements to load next room with </param>
    public static void LoadNextRoom(int amountOfElements)
    {
        // Refill element list for next rounds.
        if (availableElements.Count <= 0)
        {
            RefillAvailableElements();
        }

        string message = "Load biome with";

        CurrentElements.Clear();

        // Get random elements
        for (int i = 0; i < amountOfElements; i++)
        {
            // If to many elements were asked for break loop
            if (availableElements.Count <= 0) break;

            int rand = Random.Range(0, availableElements.Count);
            ElementMain.ElementType element = availableElements[rand];

            CurrentElements.Add(element);
            message += " " + element;

            // Remove element from options
            availableElements.RemoveAt(rand);
        }

        Debug.Log(message + " element");

    }
}

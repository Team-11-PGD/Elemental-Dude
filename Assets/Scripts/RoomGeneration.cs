using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

static public class RoomGeneration
{
    /// <summary>
    /// A list of all active elements in the level
    /// </summary>
    public static List<ElementMain.ElementType> CurrentElements { get; private set; } = new List<ElementMain.ElementType>();

    /// <summary>
    /// A array that contains all options for the next level
    /// </summary>
    public static List<ElementMain.ElementType>[] NextElements { get; private set; } = new List<ElementMain.ElementType>[PATH_OPTIONS];

    static List<ElementMain.ElementType> previousElements = new List<ElementMain.ElementType>();
    const int PATH_OPTIONS = 2;

    /// <summary>
    /// Load next level element data and scene
    /// </summary>
    /// <param name="elementTypes"> Types of elements to load the next level with </param>
    /// <param name="amountOfElementsNextRound"> Amount of elements for next level </param>
    public static void LoadNextLevel(List<ElementMain.ElementType> elementTypes, int amountOfElementsNextRound)
    {
        previousElements = CurrentElements;
        CurrentElements = elementTypes;

        // Get random elements for each path
        for (int i = 0; i < PATH_OPTIONS; i++)
        {
            List<ElementMain.ElementType> randomElements;

            do
            {
                // Create a list with all possible elements
                List<ElementMain.ElementType> availableElements = Enum.GetValues(typeof(ElementMain.ElementType)).OfType<ElementMain.ElementType>().ToList();
                availableElements.Remove(ElementMain.ElementType.None); 

                randomElements = new List<ElementMain.ElementType>();

                // Find random elements
                for (int j = 0; j < amountOfElementsNextRound; j++)
                {
                    int rand = UnityEngine.Random.Range(0, availableElements.Count);
                    randomElements.Add(availableElements[rand]);
                    availableElements.RemoveAt(rand);
                }

                // Sort the list so that different order doesn't matter when checking repeating levels
                randomElements.Sort();
            } while (randomElements == previousElements); // Make sure the new elements dont repeat the previous level

            NextElements[PATH_OPTIONS] = randomElements;
        }

        // TODO: load next level
        Debug.Log(NextElements);
    }
}

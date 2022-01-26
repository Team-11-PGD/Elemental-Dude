using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomGeneration : MonoBehaviour
{
    public static RoomGeneration instance;

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
            UpdateElements(new List<ElementMain.ElementType> { ElementMain.ElementType.None }, 1);
            UpdateElements(NextElements[0], 1);
            UpdateElements(new List<ElementMain.ElementType> { ElementMain.ElementType.Fire }, 1);
            print($"Starting with: {CurrentElements[0]}");
        }
    }

    /// <summary>
    /// A list of all active elements in the level
    /// </summary>
    public static List<ElementMain.ElementType> CurrentElements { get; private set; } = new List<ElementMain.ElementType>();

    /// <summary>
    /// A array that contains all options for the next level
    /// </summary>
    public static List<ElementMain.ElementType>[] NextElements { get; private set; } = new List<ElementMain.ElementType>[PATH_OPTIONS];

    public static int Level { get; private set; } = 0;

    [SerializeField]
    SceneReference[] levels;

    public const int MULTI_ELEMENT_LEVEL = 3;
    public const int FINAL_LEVEL = 5;
    public const int PATH_OPTIONS = 2;

    /// <summary>
    /// Load next level element data and scene
    /// </summary>
    /// <param name="elementTypes"> Types of elements to load the next level with </param>
    /// <param name="amountOfElementsNextRound"> Amount of elements for next level </param>
    public static void LoadNextLevel(List<ElementMain.ElementType> elementTypes, int amountOfElementsNextRound)
    {
        UpdateElements(elementTypes, amountOfElementsNextRound);
        elementTypes.ForEach((element) => print($"Loading: {element}"));
        NextElements[0].ForEach((element) => print($"Next 0: {element}"));
        NextElements[1].ForEach((element) => print($"Next 1: {element}"));

        // Load next level
        Level++;
        int randLevel;
        do
        {
            randLevel = UnityEngine.Random.Range(0, instance.levels.Length);
        } while (instance.levels[randLevel].ScenePath == SceneManager.GetActiveScene().path);
        Debug.Log(instance.levels[randLevel].ScenePath);
        SceneManager.LoadScene(instance.levels[randLevel]);
    }

    private static void UpdateElements(List<ElementMain.ElementType> elementTypes, int amountOfElementsNextRound)
    {
        CurrentElements = elementTypes;

        NextElements = new List<ElementMain.ElementType>[PATH_OPTIONS];

        // Get random elements for path 1
        List<ElementMain.ElementType> availableElements = Enum.GetValues(typeof(ElementMain.ElementType)).OfType<ElementMain.ElementType>().ToList();
        availableElements.Remove(ElementMain.ElementType.None);
        //availableElements.Remove(ElementMain.ElementType.Earth);

        List<ElementMain.ElementType> randomElements = new List<ElementMain.ElementType>();

        // Find random elements
        for (int i = 0; i < amountOfElementsNextRound; i++)
        {
            int rand = UnityEngine.Random.Range(0, availableElements.Count);
            randomElements.Add(availableElements[rand]);
            availableElements.RemoveAt(rand);
        }
        NextElements[0] = randomElements;
        NextElements[0].Sort();

        // Make it not the same as previous element
        int duplicate = 0;
        for (int i = 0; i < CurrentElements.Count; i++)
        {
            if (NextElements[0][i] == CurrentElements[i]) duplicate++;
        }
        if (duplicate == NextElements[0].Count)
        {
            NextElements[0].Remove(CurrentElements[UnityEngine.Random.Range(0, CurrentElements.Count)]);
            NextElements[0].Add(availableElements[UnityEngine.Random.Range(0, availableElements.Count)]);
        }
        NextElements[0].Sort();

        // Get random elements for path 2
        availableElements = Enum.GetValues(typeof(ElementMain.ElementType)).OfType<ElementMain.ElementType>().ToList();
        availableElements.Remove(ElementMain.ElementType.None);
        //availableElements.Remove(ElementMain.ElementType.Earth);
        availableElements.Remove(NextElements[0][UnityEngine.Random.Range(0, NextElements[0].Count)]);

        randomElements = new List<ElementMain.ElementType>();

        // Find random elements
        for (int j = 0; j < amountOfElementsNextRound; j++)
        {
            int rand = UnityEngine.Random.Range(0, availableElements.Count);
            randomElements.Add(availableElements[rand]);
            availableElements.RemoveAt(rand);
        }
        NextElements[1] = randomElements;
        NextElements[1].Sort();

        // Make it not the same as previous element
        duplicate = 0;
        for (int i = 0; i < CurrentElements.Count; i++)
        {
            if (NextElements[1][i] == CurrentElements[i]) duplicate++;
        }
        if (duplicate == NextElements[1].Count)
        {
            NextElements[1].Remove(CurrentElements[UnityEngine.Random.Range(0, CurrentElements.Count)]);
            NextElements[1].Add(availableElements[UnityEngine.Random.Range(0, availableElements.Count)]);
        }
        NextElements[1].Sort();
    }
}
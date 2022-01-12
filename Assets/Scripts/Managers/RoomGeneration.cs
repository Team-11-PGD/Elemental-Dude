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
            UpdateElements(new List<ElementMain.ElementType> { ElementMain.ElementType.Fire }, 1);
            UpdateElements(NextElements[0], 1);
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

    static List<ElementMain.ElementType> previousElements = new List<ElementMain.ElementType>();
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

    [ContextMenu("Update Elements")]
    public void Test()
    {
        for (int i = 0; i < 1; i++)
        {
            UpdateElements(new List<ElementMain.ElementType> { ElementMain.ElementType.Fire }, 1);
            int duplicate = 0;
            for (int j = 0; j < 1; j++)
            {
                if (NextElements[0][j] == NextElements[1][j]) duplicate++;
            }
            if (duplicate == 1)
            {
                NextElements[0].ForEach((element) => print($"Next 0: {element}"));
                NextElements[1].ForEach((element) => print($"Next 1: {element}"));
            }

            UpdateElements(NextElements[0], 1);
            duplicate = 0;
            for (int j = 0; j < 1; j++)
            {
                if (NextElements[0][j] == NextElements[1][j]) duplicate++;
            }
            if (duplicate == 1)
            {
                NextElements[0].ForEach((element) => print($"Next 0: {element}"));
                NextElements[1].ForEach((element) => print($"Next 1: {element}"));
            }
        }
    }

    private static void UpdateElements(List<ElementMain.ElementType> elementTypes, int amountOfElementsNextRound)
    {
        previousElements = CurrentElements;
        CurrentElements = elementTypes;

        NextElements = new List<ElementMain.ElementType>[PATH_OPTIONS];

        int duplicate;
        do
        {
            for (int i = 0; i < PATH_OPTIONS; i++)
            {
                int previousDuplicate;
                do
                {
                    // Get random elements for each path
                    List<ElementMain.ElementType> availableElements = Enum.GetValues(typeof(ElementMain.ElementType)).OfType<ElementMain.ElementType>().ToList();
                    availableElements.Remove(ElementMain.ElementType.None);
                    availableElements.Remove(ElementMain.ElementType.Earth);
                    List<ElementMain.ElementType> randomElements = new List<ElementMain.ElementType>();

                    // Find random elements
                    for (int j = 0; j < amountOfElementsNextRound; j++)
                    {
                        int rand = UnityEngine.Random.Range(0, availableElements.Count);
                        randomElements.Add(availableElements[rand]);
                        availableElements.RemoveAt(rand);
                    }
                    randomElements.Sort();

                    NextElements[i] = randomElements;

                    // Check if options are same
                    previousDuplicate = 0;
                    for (int k = 0; k < previousElements.Count; k++)
                    {
                        if (NextElements[i][k] == previousElements[k]) previousDuplicate++;
                    }
                } while (previousDuplicate == previousElements.Count);
            }

            // Check if options are same
            duplicate = 0;
            for (int i = 0; i < amountOfElementsNextRound; i++)
            {
                if (NextElements[0][i] == NextElements[1][i]) duplicate++;
            }
        } while (duplicate == amountOfElementsNextRound);
    }
}

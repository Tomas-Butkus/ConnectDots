using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private JSONReader jSONReader;
    [SerializeField] private GameObject dotPrefab;

    private Levels levelsCollection;
    private int currentLevelIndex = 0;

    public int currentDotIndex;
    public static GameManager Instance;

    private void Awake()
    {
        SetupSingleton();
    }

    private void Start()
    {
        SetupLevels();
        SetupDots();
    }

    // Read JSON file, save reference to levels and extract their coordinates
    private void SetupLevels()
    {
        levelsCollection = jSONReader.ReadLevelsFile();

        foreach (LevelData levelData in levelsCollection.levels)
        {
            levelData.ExtractCoordinates();
        }
    }

    // Setup dots depending on the level
    private void SetupDots()
    {
        // Get current level data
        LevelData currentLevelData = levelsCollection.levels[currentLevelIndex];

        // Create a parent for dots
        GameObject dotsDiagram = new GameObject();
        dotsDiagram.name = "DotsDiagram";
        dotsDiagram.transform.position = transform.position;

        // Populate dots in the level and set their parent
        for (int i = 0; i < currentLevelData.level_data.Count / 2; i++)
        {
            int xCoordinate = currentLevelData.xCoordinates[i] / 100;
            int yCoordinate = currentLevelData.yCoordinates[i] / 100;

            GameObject dot = Instantiate(dotPrefab);
            dot.transform.position = new Vector3(xCoordinate, yCoordinate);
            dot.transform.parent = dotsDiagram.transform;
            dot.GetComponent<Dot>().orderIndex = i + 1;

            dot.GetComponentInChildren<TextMeshPro>().text = (i + 1).ToString();
        }
    }

    // Make sure only one game manager script exists in game
    private void SetupSingleton()
    {
        Instance = this;
    }
}

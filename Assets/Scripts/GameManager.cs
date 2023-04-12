using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private JSONReader jSONReader;
    [SerializeField] private GameObject dotPrefab;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject buttonParent;

    private Levels levelsCollection;

    public static int currentLevelIndex;
    public float endGameTimer = 10f;
    public List<Dot> dotList;
    public int currentDotIndex;
    public static GameManager Instance;

    private void Awake()
    {
        SetupSingleton();
        SetupLevels();
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            SetupDots();
        }
        else
        {
            SetupLevelDisplay();
        }
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
        LevelData currentLevelData = levelsCollection.levels[currentLevelIndex - 1];

        // Create a parent for dots
        GameObject dotsDiagram = new GameObject();
        dotsDiagram.name = "DotsDiagram";
        dotsDiagram.transform.position = transform.position;

        // Populate dots in the level and set their parent
        for (int i = 0; i < currentLevelData.level_data.Count / 2; i++)
        {
            Vector3 worldpointCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(currentLevelData.xCoordinates[i], currentLevelData.yCoordinates[i], 0));

            float xCoordinate = worldpointCoordinates.x;
            float yCoordinate = worldpointCoordinates.y;
            yCoordinate = -yCoordinate;

            GameObject dot = Instantiate(dotPrefab);
            dot.transform.position = new Vector3(xCoordinate, yCoordinate, 0);
            dot.transform.parent = dotsDiagram.transform;
            dot.GetComponent<Dot>().orderIndex = i + 1;
            dot.GetComponentInChildren<TextMeshPro>().text = (i + 1).ToString();

            dotList.Add(dot.GetComponent<Dot>());
        }
    }

    // Load level selection scene
    public void LoadLevelSelectionScreen()
    {
        SceneManager.LoadScene(0);
    }

    // Load selected level
    public void LoadLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        SceneManager.LoadScene(1);
    }

    // Display levels on screen
    private void SetupLevelDisplay()
    {
        for (int i = 0; i < levelsCollection.levels.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, buttonParent.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
        }
    }

    // Make sure only one game manager script exists in game
    private void SetupSingleton()
    {
        Instance = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private JSONReader jSONReader;
    [SerializeField] private GameObject dotPrefab;

    private Levels levelsCollection;
    private int currentLevelIndex = 0;

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

    private void SetupLevels()
    {
        levelsCollection = jSONReader.ReadLevelsFile();

        foreach (LevelData levelData in levelsCollection.levels)
        {
            levelData.ExtractCoordinates();
        }
    }

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
            int xCoordinate = currentLevelData.xCoordinates[i];
            int yCoordinate = currentLevelData.yCoordinates[i];

            GameObject dot = Instantiate(dotPrefab);
            dot.transform.localPosition = new Vector3(xCoordinate, yCoordinate);
            dot.transform.parent = dotsDiagram.transform;

            
        }
    }

    // Make sure only one game manager script exists in game
    private void SetupSingleton()
    {
        Instance = this;
    }
}

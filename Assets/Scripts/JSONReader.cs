using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    [SerializeField] private TextAsset jSONFile;

    public Levels ReadLevelsFile()
    {
        Levels levelsInJson = JsonUtility.FromJson<Levels>(jSONFile.text);

        return levelsInJson;
    }
}

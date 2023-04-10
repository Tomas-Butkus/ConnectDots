using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public List<string> level_data;
    public List<int> xCoordinates = new List<int>();
    public List<int> yCoordinates = new List<int>();

    // Extract dot coordinates from level data
    public void ExtractCoordinates()
    {
        for (int i = 0; i < level_data.Count; i += 2)
        {
            xCoordinates.Add(int.Parse(level_data[i]));
            yCoordinates.Add(int.Parse(level_data[i + 1]));
        }
    }
}

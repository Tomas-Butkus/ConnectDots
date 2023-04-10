using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        levelIndex = int.Parse(GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text);
        GameManager.Instance.LoadLevel(levelIndex);
    }
}

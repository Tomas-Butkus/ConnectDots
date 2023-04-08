using TMPro;
using UnityEngine;

public class ConnectDots : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CheckMouseInput();
    }

    // Check if left mouse button was clicked
    private void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForDots();
        }
    }

    // Check for a dot
    private void CheckForDots()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if(hit.collider != null && hit.transform.gameObject.tag == "Dot")
        {
            ConnectTheDot(hit.transform.gameObject.GetComponent<Dot>());
        }
    }

    // Connect the dots if they are in sequence
    private void ConnectTheDot(Dot dot)
    {
        if(!dot.isConnected && gameManager.currentDotIndex + 1 == dot.orderIndex)
        {
            dot.isConnected = true;
            dot.GetComponentInChildren<TextMeshPro>().enabled = false;
            gameManager.currentDotIndex++;
        }
    }
}

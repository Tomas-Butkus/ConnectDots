using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private Sprite connectedDot;
    [SerializeField] private Sprite unconnectedDot;

    public int orderIndex;
    public bool isConnected = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = unconnectedDot;
    }

    private void Update()
    {
        CheckIfConnected();
    }

    // Check if dot is connected and update sprite
    private void CheckIfConnected()
    {
        if(isConnected)
        {
            spriteRenderer.sprite = connectedDot;
        }
    }
}

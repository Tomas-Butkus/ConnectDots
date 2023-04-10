using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ConnectDots : MonoBehaviour
{
    [SerializeField] private float ropeSpeed;
    [SerializeField] private float fadeOutDuration;
    private bool isConnectingRope = false;

    private Queue<VectorPair> ropeConnectionQueue = new Queue<VectorPair>();

    private void Update()
    {
        CheckMouseInput();
        ConnectRopeConnection();
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
        if(!dot.isConnected && GameManager.Instance.currentDotIndex + 1 == dot.orderIndex)
        {
            dot.isConnected = true;
            StartCoroutine(FadeOutText(dot.GetComponentInChildren<TextMeshPro>(), fadeOutDuration));
            //dot.GetComponentInChildren<TextMeshPro>().enabled = false;
            GameManager.Instance.currentDotIndex++;

            if(dot.orderIndex != 1)
            {
                if (dot.orderIndex != GameManager.Instance.dotList.Count)
                {
                    Vector3 startPos = GameManager.Instance.dotList[dot.orderIndex - 2].transform.position;
                    Vector3 endPos = dot.transform.position;

                    VectorPair ropeConnection = new VectorPair(startPos, endPos, dot.orderIndex);
                    ropeConnectionQueue.Enqueue(ropeConnection);
                }
                else
                {
                    Vector3 startPos = GameManager.Instance.dotList[dot.orderIndex - 2].transform.position;
                    Vector3 endPos = dot.transform.position;

                    VectorPair ropeConnection = new VectorPair(startPos, endPos, dot.orderIndex);
                    ropeConnectionQueue.Enqueue(ropeConnection);

                    FinishTheDiagram(dot.transform.position);
                }
            }
        }
    }

    // Fade out animation for text
    private IEnumerator FadeOutText(TextMeshPro text, float duration)
    {
        float alpha = 1f;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / duration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }
        text.enabled = false;
    }

    // Connect last and first dot
    private void FinishTheDiagram(Vector3 startPosition)
    {
        Vector3 startPos = startPosition;
        Vector3 endPos = GameManager.Instance.dotList.First().transform.position;

        VectorPair lastRopeConnetion = new VectorPair(startPos, endPos, 1);
        ropeConnectionQueue.Enqueue(lastRopeConnetion);

        GameManager.Instance.Invoke("LoadLevelSelectionScreen", GameManager.Instance.endGameTimer);
    }

    // Start connecting rope
    private void ConnectRopeConnection()
    {
        if (!isConnectingRope && ropeConnectionQueue.Count > 0)
        {
            VectorPair connection = ropeConnectionQueue.Dequeue();
            StartCoroutine(AnimateRope(connection));
        }
    }

    // Animate the rope to connect the dots
    IEnumerator AnimateRope(VectorPair connection)
    {
        LineRenderer rope = GameManager.Instance.dotList[connection.dotIndex - 1].GetComponent<LineRenderer>();

        isConnectingRope = true;
        Vector3 currentPos = connection.startVector;
        Vector3 targetPos = connection.endVector;

        rope.enabled = true;
        rope.positionCount = 2;
        rope.SetPosition(0, currentPos);
        rope.SetPosition(1, targetPos);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * ropeSpeed;
            Vector3 newPosition = Vector3.Lerp(currentPos, targetPos, t);
            rope.SetPosition(1, newPosition);

            yield return null;
        }

        rope.SetPosition(1, targetPos);
        isConnectingRope = false;
    }
}

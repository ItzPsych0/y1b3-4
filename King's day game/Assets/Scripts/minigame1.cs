using UnityEngine;

public class minigame1 : MonoBehaviour
{
    private Vector3 dragStartPos;
    private Rigidbody rb;
    private bool isDragging = false;

    [Header("Launch Settings")]
    public float forceMultiplier = 10f;
    public float stopVelocityThreshold = 0.1f;

    public int Score = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (rb.linearVelocity.magnitude < stopVelocityThreshold)
        {
            dragStartPos = Input.mousePosition;
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            Vector3 dragEndPos = Input.mousePosition;
            Vector3 force = dragStartPos - dragEndPos;
            Vector3 direction = new Vector3(force.x, 0, force.y);
            rb.AddForce(direction * forceMultiplier);
            isDragging = false;
        }
    }

    public void ScorePoints(int points)
    {
        Score += points;
        
        Debug.Log("Scored a total of " + Score + " points!");



    }

   
}

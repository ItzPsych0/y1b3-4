using UnityEngine;

public class minigame1 : MonoBehaviour
{
    private Vector3 dragStartPos;
    private Rigidbody rb;
    private bool isDragging = false;

    [Header("Launch Settings")]
    public float forceMultiplier = 10f;
    public float stopVelocityThreshold = 0.1f;

    [Header("Scoring Settings")]
    public int hole1Points = 2;
    public int hole2Points = 3;
    public int hole3Points = 4;
    public int hole4Points = 1;

    [Header("Respawn Settings")]
    public Transform puckSpawnPoint;
    public GameObject puckPrefab;
    public bool respawnOnScore = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hole1"))
        {
            ScorePoints(hole1Points);
        }
        else if (other.CompareTag("Hole2"))
        {
            ScorePoints(hole2Points);
        }
        else if (other.CompareTag("Hole3"))
        {
            ScorePoints(hole3Points);
        }
        else if (other.CompareTag("Hole4"))
        {
            ScorePoints(hole4Points);
        }
    }

    private void ScorePoints(int points)
    {
        
        
        Debug.Log("Scored " + Score+points + " points!");


        // Optionally add points to a global score manager here

        if (respawnOnScore && puckPrefab != null && puckSpawnPoint != null)
        {
            Instantiate(puckPrefab, puckSpawnPoint.position, puckSpawnPoint.rotation);
        }

        Destroy(gameObject); // Remove current puck
    }

   
}

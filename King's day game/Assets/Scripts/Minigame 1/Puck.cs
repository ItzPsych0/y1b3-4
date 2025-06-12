
using UnityEngine;

public class Puck : MonoBehaviour
{
    private Rigidbody rb;
    private bool launched = false;
    private bool isDragging = false;
    private Vector3 dragStart;
    private bool hasNotified = false;
    public float forceMultiplier = 5f;
    public float lineLength = 0.5f;

    private float stopTimer = 0f;
    private float stopDelay = 1f;


    public float stopVelocityThreshold = 0.1f;

    public LineRenderer lineRenderer;
    void Start()
    {
        // Activates the pucks RigidBody
        rb = GetComponent<Rigidbody>();

        // If no lineReader is assigned, assign it
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.enabled = false;
    }
    
    void Update()
    {
        // Check if puck has been launched and hasn't been marked as stopped
        if (launched && !hasNotified)
        {
            if (rb.linearVelocity.magnitude < 0.1f)
            {
                stopTimer += Time.deltaTime;

                // If it's been stopped long enough, mark as fully stopped
                if (stopTimer >= stopDelay)
                {
                    hasNotified = true;
                }
            }
            else
            {
                stopTimer = 0f;
            }
        }
    }

    //When player clicks on the puck
    private void OnMouseDown()
    {
        Debug.Log("Puck clicked!");
        // Only allow aiming if the puck is currently stopped
        if (rb.linearVelocity.magnitude < stopVelocityThreshold)
        {
            dragStart = Input.mousePosition;
            isDragging = true;
            lineRenderer.enabled = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 dragCurrent = Input.mousePosition; //Current mouse position(while dragging)
            Vector3 force = dragStart - dragCurrent; //calculate distance dragged
            Vector3 direction = new Vector3(force.y, 0, force.x).normalized; // Convert screen drag to world direction

            // Calculate start and end point for line
            Vector3 start = transform.position;
            Vector3 end = start + direction * lineLength;

            // Set the 2 points for LineRenderer to draw line
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            Vector3 dragEndPos = Input.mousePosition;
            Vector3 force = dragStart - dragEndPos;
            Vector3 direction = new Vector3(force.y, 0, force.x);
            rb.AddForce(direction * forceMultiplier);
            isDragging = false;
            lineRenderer.enabled = false;
        }
    }


}

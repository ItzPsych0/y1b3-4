
using UnityEngine;

public class Puck : MonoBehaviour
{

    private float dragDeadZone = 10f;            // Ignore tiny mouse movements
    private Vector3 smoothedDirection = Vector3.forward;
    public float smoothingSpeed = 10f;           // Higher = faster smoothing

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
            Vector3 dragCurrent = Input.mousePosition;
            Vector3 dragVector = dragStart - dragCurrent;

            // Only update aiming direction if drag is large enough
            if (dragVector.magnitude > dragDeadZone)
            {
                Vector3 targetDirection = new Vector3(dragVector.y, 0, dragVector.x).normalized;

                // Smoothly interpolate toward target direction
                smoothedDirection = Vector3.Lerp(smoothedDirection, targetDirection, Time.deltaTime * smoothingSpeed);
            }

            // Always update line with the current smoothed direction
            Vector3 start = transform.position;
            Vector3 end = start + smoothedDirection * lineLength;

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

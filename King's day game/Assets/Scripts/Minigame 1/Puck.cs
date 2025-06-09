using Unity.VisualScripting;
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

  

    [Header("Launch Settings")]
    public float stopVelocityThreshold = 0.1f;

    public LineRenderer lineRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.enabled = false;
    }
    
    void Update()
    {
        if (launched && !hasNotified)
        {
            if (rb.linearVelocity.magnitude < 0.1f)
            {
                stopTimer += Time.deltaTime;

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

    private void OnMouseDown()
    {
        Debug.Log("Puck clicked!");
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
            Vector3 force = dragStart - dragCurrent;
            Vector3 direction = new Vector3(force.y, 0, force.x).normalized;

            Vector3 start = transform.position;
            Vector3 end = start + direction * lineLength;

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

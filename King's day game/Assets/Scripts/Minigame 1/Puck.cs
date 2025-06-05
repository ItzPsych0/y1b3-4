using Unity.VisualScripting;
using UnityEngine;

public class Puck : MonoBehaviour
{
    private Rigidbody rb;
    private bool launched = false;
    private Vector3 dragStart;
    private bool hasNotified = false;
    public float forceMultiplier = 5f;
    private float stopTimer = 0f;
    private float stopDelay = 1f;

    private bool isDragging = false;

    [Header("Launch Settings")]
    public float stopVelocityThreshold = 0.1f;

    public LineRenderer lineRenderer;
    public int linePoints = 30;          // How many points to calculate
    public float timeBetweenPoints = 0.1f; // Time step between each point
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
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
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            Vector3 dragEndPos = Input.mousePosition;
            Vector3 force = dragStart - dragEndPos;
            Vector3 direction = new Vector3(force.x, 0, force.y);
            rb.AddForce(direction * forceMultiplier);
            isDragging = false;
        }
    }

    void ShowTrajectory(Vector3 startPosition, Vector3 initialVelocity)
    {
        lineRenderer.positionCount = linePoints;
        Vector3[] points = new Vector3[linePoints];

        for (int i = 0; i < linePoints; i++)
        {
            float t = i * timeBetweenPoints;
            Vector3 point = startPosition + initialVelocity * t + 0.5f * Physics.gravity * t * t;
            points[i] = point;
        }

        lineRenderer.SetPositions(points);
        lineRenderer.enabled = true;
    }




}

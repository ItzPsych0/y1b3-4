using Unity.VisualScripting;
using UnityEngine;

public class Puck : MonoBehaviour
{
    private Rigidbody rb;
    private bool launched = false;
    private Vector3 dragStart;
    private bool hasNotified = false;
    private minigame1    minigame1;
    public float forceMultiplier = 10f;
    private float stopTimer = 0f;
    private float stopDelay = 1f;

    private bool isDragging = false;

    [Header("Launch Settings")]
    public float stopVelocityThreshold = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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


    //void OnMouseDown()
    //{
    //    if (!launched)
    //    {
    //        dragStart = Input.mousePosition;
    //    }
    //}

    //void OnMouseUp()
    //{
    //    if (!launched)
    //    {
    //        Vector3 dragEnd = Input.mousePosition;
    //        Vector3 force = dragStart - dragEnd;
    //        Vector3 launchDir = new Vector3(force.x, 0, force.y);
    //        rb.AddForce(launchDir * forceMultiplier);
    //        launched = true;
    //    }
    //}

}

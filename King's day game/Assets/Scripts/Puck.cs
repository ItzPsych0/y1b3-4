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

    void OnMouseDown()
    {
        if (!launched)
        {
            dragStart = Input.mousePosition;
        }
    }

    void OnMouseUp()
    {
        if (!launched)
        {
            Vector3 dragEnd = Input.mousePosition;
            Vector3 force = dragStart - dragEnd;
            Vector3 launchDir = new Vector3(force.x, 0, force.y);
            rb.AddForce(launchDir * forceMultiplier);
            launched = true;
        }
    }
   
}

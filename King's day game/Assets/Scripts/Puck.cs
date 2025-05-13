using Unity.VisualScripting;
using UnityEngine;

public class Puck : MonoBehaviour
{
    private Rigidbody rb;
    private bool launched = false;
    private Vector3 dragStart;
    private bool hasNotified = false;
    private GameInteract gameManager;
    public float forceMultiplier = 10f;
    private float stopTimer = 0f;
    private float stopDelay = 1f;

    public void Setup(GameInteract gm)
    {
        gameManager = gm;
    }

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
                    gameManager.NotifyPuckGone();
                    Destroy(gameObject, 1f);
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

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Hole1": gameManager.AddScore(1); break;
            case "Hole2": gameManager.AddScore(2); break;
            case "Hole3": gameManager.AddScore(3); break;
            case "Hole4": gameManager.AddScore(4); break;
        }

    }

   
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WanderingNPC : MonoBehaviour
{
    public float moveSpeed = 2f;             // movement speed
    public float directionChangeInterval = 4f; // time between direction changes
    public float maxTurnAngle = 360f;         // maximum angle change in degrees

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float directionChangeTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; //we stop all kinds or rotation to the agent
        PickNewDirection();
    }

    void FixedUpdate()
    {
        // movement in the current direction
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        // timer countdown
        directionChangeTimer -= Time.fixedDeltaTime;
        if (directionChangeTimer <= 0f)
        {
            PickNewDirection();
        }
    }

    void PickNewDirection()
    {
        //randomized direction on the XZ axis
        float angle = Random.Range(-maxTurnAngle, maxTurnAngle);
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        moveDirection = rotation * transform.forward;
        directionChangeTimer = directionChangeInterval;
    }
}

using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public float movespeed = 5f;
    public float jumpForce = 5f;
    Rigidbody rb;

    bool isGrounded = false;
    public Transform groundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(groundedChecker.position, checkGroundRadius, groundLayer);

        var input = new Vector3(x, 0, y);
        transform.position += input * movespeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity += new Vector3(0, jumpForce, 0);
        }
    }
}

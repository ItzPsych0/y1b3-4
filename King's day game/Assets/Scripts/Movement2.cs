using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public float movespeed = 5f;
    public float jumpForce = 5f;
    Rigidbody rb;

    void Start()
    {
        // Rigidbody
        rb = GetComponent<Rigidbody>();
    }
        // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3(x, 0, y);
        transform.position += input * movespeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity += new Vector3(0, jumpForce, 0);
        }
    }
}

using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 3f;
    public Transform player; 
    public Transform target; 
    public Vector2 look;
    public float smoothing = 0.1f;
    private Vector3 offset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - target.position;
    }

    void Update()
    {
        LookUpdate();
        FollowTarget();
    }

    private void LookUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        look.x += mouseX;
        look.y -= mouseY;

        look.y = Mathf.Clamp(look.y, -25f, 20f);

        transform.localRotation = Quaternion.Euler(look.y, look.x, 0);
        player.rotation = Quaternion.Euler(0, look.x, 0);
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = target.position + player.rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothing);
    }

}

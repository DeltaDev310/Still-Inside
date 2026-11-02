
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    public float mouseSensitivity = 150f;
    public float maxLookAngle = 85f;

    private float xRotation = 0f;
    private Transform playerBody;

    void Start()
    {
        playerBody = transform.parent;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleCamera();
    }

    void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation: camera only
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation: player body
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

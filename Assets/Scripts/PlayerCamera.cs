using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Sense")]
    public float sensX;
    public float sensY;

    [Header("Player Orientation")]
    public Transform PlayerOrientation;

    [Header("Camera Rotation")]
    public float xRotation;
    public float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        xRotation -= mouseY;
        yRotation += mouseX;

        //rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        PlayerOrientation.rotation = Quaternion.Euler(0, yRotation, 0); 
    }
}

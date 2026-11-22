
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerBody;

    [Header("Camera Settings")]
    [SerializeField] private float sensitivity = 0.15f;
    [SerializeField] private float minVerticalAngle = -85f;
    [SerializeField] private float maxVerticalAngle = 85f;

    [Header("Smoothing")]
    [SerializeField] private bool useSmoothing = true;
    [SerializeField] private float smoothSpeed = 15f;

    private float xRotation;
    private Vector2 currentMouseDelta;
    private Vector2 smoothMouseDelta;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleCursor();

        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        HandleCamera();
    }

    private void HandleCamera()
    {
        if (Mouse.current == null)
            return;

        currentMouseDelta = Mouse.current.delta.ReadValue() * sensitivity;

        if (useSmoothing)
        {
            smoothMouseDelta = Vector2.Lerp(
                smoothMouseDelta,
                currentMouseDelta,
                smoothSpeed * Time.deltaTime
            );
        }
        else
        {
            smoothMouseDelta = currentMouseDelta;
        }

        xRotation -= smoothMouseDelta.y;
        xRotation = Mathf.Clamp(
            xRotation,
            minVerticalAngle,
            maxVerticalAngle
        );

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(
            Vector3.up * smoothMouseDelta.x
        );
    }

    private void HandleCursor()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            bool locked = Cursor.lockState == CursorLockMode.Locked;

            Cursor.lockState = locked
                ? CursorLockMode.None
                : CursorLockMode.Locked;

            Cursor.visible = locked;
        }
    }
}


using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 6f;

    [Header("Gravity")]
    public float gravity = -20f;

    [Header("Jump Settings")]
    public float jumpHeight = 1.2f;

    [Header("Bunny Hop")]
    public float hopSpeedBonus = 0.4f;
    public float maxHopBonus = 3f;
    public float hopTimingWindow = 0.25f;

    private float currentHopBonus;
    private float lastLandingTime;
    private bool wasGrounded;

    private CharacterController controller;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        bool isGrounded = controller.isGrounded;
        if (isGrounded && !wasGrounded)
        {
            lastLandingTime = Time.time;
        }

        wasGrounded = isGrounded;
        HandleJump();
        HandleMovement();
    }

    private void HandleJump()
    {
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastLandingTime <= hopTimingWindow)
            {
                currentHopBonus = Mathf.Min(currentHopBonus + hopSpeedBonus, maxHopBonus);
                Debug.Log($"Bhop! Current speed bonus: {currentHopBonus}", this);
            }
            else
            {
                currentHopBonus = 0f;
            }

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move = Vector3.ClampMagnitude(move, 1f);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift)
            ? sprintSpeed
            : walkSpeed;
        currentSpeed += currentHopBonus;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMovement = move * currentSpeed;
        finalMovement.y = velocity.y;

        controller.Move(finalMovement * Time.deltaTime);
    }
}

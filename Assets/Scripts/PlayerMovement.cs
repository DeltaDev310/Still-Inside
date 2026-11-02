using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header ("Player")]
    public float Speed = 5;

    private Rigidbody rb;
    private Vector3 Movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float z = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");

        Movement = new Vector3(x, 0f, z).normalized;
    }

    void FixedUpdate()
    {
        Vector3 velocity = transform.TransformDirection(Movement) * Speed;

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal"); // No smoothing
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized * speed;
    }

    void FixedUpdate()
    {
        if (moveInput.magnitude > 0)
        {
            rb.linearVelocity = moveInput; // Apply movement
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Stop immediately when no keys are pressed
        }
    }
}

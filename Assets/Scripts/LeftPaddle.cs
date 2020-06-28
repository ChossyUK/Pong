using UnityEngine;

public class LeftPaddle : MonoBehaviour
{
    [SerializeField] float paddleSpeed = 10f;       // Float for Paddle Speed

    float paddlePosition = -8f;         // Float for Paddle position 
    float verticalInput;                // Float for input
    Rigidbody2D rb;                     // Rigidbody2d reference

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       // Get the rigidbody2d attached to this script 
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        verticalInput = Input.GetAxisRaw("Vertical");       // Get the vertical input
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, verticalInput * paddleSpeed);      // Move the paddle
    }

    public void ResetPaddle()
    {
        transform.position = new Vector3(paddlePosition, 0, 0);         // Reset for when the Ai or player 2 score a point
    }
}

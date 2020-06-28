using UnityEngine;

public class RightPaddle : MonoBehaviour
{
    [SerializeField] float paddleSpeed = 10f;            // Float for Paddle Speed
    [SerializeField] float aiPaddleSpeed = 10f;          // Float for AI Paddle Speed
    float paddlePosition = 8f;                           // Float for paddle position
    [SerializeField] GameObject topBounds;               // Top bounds reference for AI
    [SerializeField] GameObject bottomBounds;            // Bottom bounds reference for AI
    [SerializeField] GameObject ball;                    // Ball reference for AI
    public bool aiControlled = false;                    // Bool for Ai or Player control

    float verticalInput;                // Float for input
    Rigidbody2D rb;                     // Rigidbody2d reference
    Vector2 ballPosition;               // Ball reference for AI

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       // Get the rigidbody2d attached to this script 
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        verticalInput = Input.GetAxisRaw("p2Vertical");       // Get the vertical input
    }

    void FixedUpdate()
    {
        if (!aiControlled)      // Check if AI or Player
        {
            MovePaddle();       // If player run the player controlled method
        }
        else
        {
            AIMovePaddle();     // If computer run the AI controlled method
        }
    }

    void MovePaddle()
    {
        rb.velocity = new Vector2(rb.velocity.x, verticalInput * paddleSpeed);              // If not AI move the player with keys/joypad
    }

    void AIMovePaddle()
    {
        // Get the balls current position
        ballPosition = ball.transform.localPosition;

        // Check if the paddle position is greater than the bottom bounds position & the ball position is less than the paddle position
        if (transform.localPosition.y > bottomBounds.transform.position.y && ballPosition.y < transform.localPosition.y)
        {
            // Move the paddle downwards
            transform.localPosition += new Vector3(0, -aiPaddleSpeed * Time.fixedDeltaTime, 0);
        }

        // Check if the paddle position is less than the top bounds position & the ball position is greater than the paddle position
        if (transform.localPosition.y < topBounds.transform.position.y && ballPosition.y > transform.localPosition.y)
        {
            // Move the paddle upwards
            transform.localPosition += new Vector3(0, aiPaddleSpeed * Time.fixedDeltaTime, 0);
        }
    }

    public void AiEnabled(bool isAI)
    {
        aiControlled = isAI;
    }

    public void ResetPaddle()
    {
        transform.position = new Vector3(paddlePosition, 0, 0);
    }
}

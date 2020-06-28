using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] float ballSpeed = 3f;             // Float for Ball Speed
    [SerializeField] float resetBallSpeed = 3f;        // Float to reset Ball Speed
    [SerializeField] float speedIncrease = 0.2f;       // Float amount to increase Ball Speed
    [SerializeField] AudioClip paddlesound;            // Paddle sound effect
    [SerializeField] AudioClip walllsound;              // Wall sound effect

    Rigidbody2D rb;                                    // Rigidbody2d reference

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       // Get the rigidbody2d attached to this script
    }


    public void RespawnBall()
    {
        StartCoroutine("SpawnBall");
    }


    void OnCollisionEnter2D(Collision2D collision)                                              // Check for collision with paddles
    {
        if (collision.gameObject.name == "LeftPaddle")                                          // Left paddle
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0.5f)             // If moving upwards
            {
                rb.velocity = new Vector2(ballSpeed, ballSpeed);                                // Return the ball in an upwards diagonal direction
            }
            else if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < -0.5f)       // If moving downards
            {
                rb.velocity = new Vector2(ballSpeed, -ballSpeed);                               // Return the ball in an downwards diagonal direction
            }

            ballSpeed += speedIncrease;                                                         // Increase balls speed
            AudioManager.instance.PlaySFXOneShot(paddlesound);                                  // Play the paddle sound effect
        }

        if (collision.gameObject.name == "RightPaddle")                                         // Right paddle
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0.5f)             // If moving upwards
            {
                rb.velocity = new Vector2(-ballSpeed, ballSpeed);                               // Return the ball in an upwards diagonal direction
            }
            else if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < -0.5f)       // If moving downards
            {
                rb.velocity = new Vector2(-ballSpeed, -ballSpeed);                              // Return the ball in an downwards diagonal direction
            }

            ballSpeed += speedIncrease;                                                         // Increase balls speed
            AudioManager.instance.PlaySFXOneShot(paddlesound);                                  // Play the paddle sound effect
        }

        //if (collision.gameObject.tag == "Wall")                                                 // If collides with wall play a sfx
        //{
        //    AudioManager.instance.PlaySFXOneShot(wallSound);                                    // Play the wall sound effect
        //}

        if (collision.gameObject.tag == "Wall")
        {
            AudioManager.instance.PlaySFXOneShot(walllsound);
        }
    }

    IEnumerator SpawnBall()
    {
        transform.position = new Vector3(0, 0, 0);              // Reset the balls transform
        int directionX = Random.Range(-1, 2);                   // Get random value for x direction
        int directionY = Random.Range(-1, 2);                   // Get random value for y direction
        if (directionX == 0) directionX = 1;                    // If random number = 0 change it
        if (directionY == 0) directionY = -1;                   // If random number = 0 change it
        rb.velocity = new Vector2(0, 0);                        // Stop the ball velocity
        ballSpeed = resetBallSpeed;                             // Reset the ball speed
        yield return new WaitForSeconds(1f);                    // Wait for a set amount of time
        rb.velocity = new Vector2(ballSpeed * directionX, ballSpeed * directionY);      // Spawn ball in random direction
    }
}

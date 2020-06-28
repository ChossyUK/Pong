using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using CedarWoodSoftware.Scene;

public class Pong : MonoBehaviour
{
    #region Public Varaibles
    [Header("Text Boxes")]
    [SerializeField] TextMeshProUGUI playerOneScore;
    [SerializeField] TextMeshProUGUI playerTwoScore;
    [SerializeField] TextMeshProUGUI winnerText;

    [Header("Player & Ball References")]
    [SerializeField] LeftPaddle leftPaddle;
    [SerializeField] RightPaddle rightPaddle;
    [SerializeField] Ball ball;
    [SerializeField] int winningScore;

    [Header("Ball Bounds")]
    [SerializeField] float leftBounds;
    [SerializeField] float rightBounds;
    [SerializeField] AudioClip loseSound;

    [Header("Menus")]
    [SerializeField] MenuController menuSwitcher;

    bool wonGame = false;
    bool singlePlayer = false;
    int player1Score = 0;
    int player2Score = 0;
    #endregion

    #region Unity Methods
    void Start()
    {
        LoadMainMenu();                                             // Load the main menu
    }

    void Update()
    {
        playerOneScore.text = player1Score.ToString();              // Update player 1 score
        playerTwoScore.text = player2Score.ToString();              // Update player 2 score

        if (ball.transform.position.x >= rightBounds)               // Check if ball has passed the right hand side of the screen
        {
            AudioManager.instance.PlaySFXOneShot(loseSound);        // Play the lose sound effect
            player1Score++;                                         // Increase player 1 score
            leftPaddle.ResetPaddle();                               // Reset the left paddle
            rightPaddle.ResetPaddle();                              // Reset the right paddle
            ball.RespawnBall();                                     // Reset the ball
        }

        if (ball.transform.position.x <= leftBounds)                // Check if ball has passed the left hand side of the screen
        {
            AudioManager.instance.PlaySFXOneShot(loseSound);
            player2Score++;
            leftPaddle.ResetPaddle();
            rightPaddle.ResetPaddle();
            ball.RespawnBall();
        }
    }

    void FixedUpdate()
    {
        if(player1Score >= winningScore)                            // Check the players score against the winning score & open the game won menu
        {
            winnerText.text = "Player One Wins";                    // Set the text to player 1 won
            LoadGameWonMenu();
        }
        else if (player2Score >= winningScore)
        {
            winnerText.text = "Player Two Wins";                    // Set the text to player 2 won
            LoadGameWonMenu();
        }

        if (Input.GetButtonDown("Cancel"))                          // Check for input to pause the game
        {
            PauseGame();
        }
    }
    #endregion

    #region Gameplay Methods

    public void SinglePlayerGame()
    {
        singlePlayer = true;                                        // Set single player game to true
        player1Score = 0;                                           // Reset the scores
        player2Score = 0;
        leftPaddle.ResetPaddle();                                   // Reset the paddles
        rightPaddle.ResetPaddle();
        rightPaddle.aiControlled = true;                            // Turn on the computer player
        UnLoadMainMenu();                                           // Unload the main menu
        ball.RespawnBall();                                         // Spawn the ball
    }

    public void TwoPlayerGame()
    {
        singlePlayer = false;                                       // Set single player to false
        player1Score = 0;
        player2Score = 0;
        leftPaddle.ResetPaddle();
        rightPaddle.ResetPaddle();
        rightPaddle.aiControlled = false;
        UnLoadMainMenu();
        ball.RespawnBall();
    }

    public void RestartGame()
    {
        if(singlePlayer)                                            // Reset the game mode
        {
            SinglePlayerGame();
        }
        else
        {
            TwoPlayerGame();
        }
    }

    public void QuitGame()
    {
        Application.Quit();                                         // Exit the game
    }

    #endregion

    #region Menu Methods

    public void LoadMainMenu()
    {
        singlePlayer = false;
        menuSwitcher.OpenMainMenu();
        Time.timeScale = 0;
    }
    public void UnLoadMainMenu()
    {
        menuSwitcher.CloseMainMenu();
        Time.timeScale = 1;
    }

    public void LoadGameWonMenu()
    {
        menuSwitcher.OpenGameOverMenu();
        Time.timeScale = 0;
    }

    public void UnLoadGameWonMenu()
    {
        menuSwitcher.CloseGameOverMenu();
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        menuSwitcher.OpenPauseMenu();
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        menuSwitcher.ClosePauseMenu();
        Time.timeScale = 1;
    }

    #endregion
}

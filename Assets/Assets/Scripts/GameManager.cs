using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // PUBLIC VARIABLES
    // GAME OBJECTS
    [Header("Ball")]
    public GameObject ball;
    
    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    [Header("Goals")]
    public GameObject player1Goal;
    public GameObject player2Goal;

    [Header("User Interface")]
    public GameObject scoreBoard;

    // PRIVATE VARIABLES
    private TextMeshProUGUI scoreText;

    private int player1Score = 0;
    private int player2Score = 0;

    private void Start()
    {
        // COMPONENTS
        scoreText = scoreBoard.GetComponent<TextMeshProUGUI>();

        // Setting up the scoreboard
        scoreText.text = $"{player1Score} - {player2Score}";
    }

    //  Runs whenever the ball collides with either goal
    public void PlayerScored(int playerNum) // playerNum represents the player's number who scored
    {
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0); // ball stop

        // Adding the scores
        if (playerNum == 1)
        {
            player1Score++;
        } else { // 2 Should be the only possible value of playerNum other than 1
            player2Score++;
        }

        scoreBoard.GetComponent<TextMeshProUGUI>().text = $"{player1Score} - {player2Score}"; // Updates the score

        // Ends the game or resets the round
        if (player1Score > 9 || player2Score > 9) // Runs when either player scores reaches 10, or because of some most-likely unintended reason, over 10
        {
            GameEnd();
        } else { // Runs when both player's scores still has not reached 10 or over
            Invoke("Reset",1);
        }
        

    }

    // Just runs the round setup functions of the major game objects
    public void Reset()
    {
        player1.GetComponent<Player>().RoundStart();
        player2.GetComponent<Player>().RoundStart();
        ball.GetComponent<Ball>().RoundStart();
    }

    // Runs whenever either player's score reaches 10 or above
    public void GameEnd()
    {
        // Shows results of game when it finishes
        if (player2.GetComponent<Player>().isAI == true) // Checks if this is a single-player match
        {
            scoreText.text = player1Score > 9 ? "NOT A BOOMER!\nCONGRATS" : "Lost to a bot?!\nYikes, you a BIG bOOMER"; // Hope you ain't no boomer, old man
        } else { // Runs when its a two-player match
            scoreText.text = player1Score > 9 ? "Player 2 is Big Boomer" : "Player 1 is Big Boomer"; // FIGHT FOR THE TITLE OF "NON-BOOMER"
        }

        Invoke("returnToMainMenu", 3); // 3 second delay before launching the main menu
        
    }

    // Loads the main menu
    public void returnToMainMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}

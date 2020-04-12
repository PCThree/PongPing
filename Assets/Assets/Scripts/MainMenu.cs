using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Loads a single player match
    public void OnePlayerGame()
    {
        SceneManager.LoadScene("1PlayerGame");
    }

    // Loads a two player match
    public void TwoPlayerGame()
    {
        SceneManager.LoadScene("2PlayerGame"); 
    }

    // Closes game
    public void Exit()
    {
        Application.Quit();
    }
}

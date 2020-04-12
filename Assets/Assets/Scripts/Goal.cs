using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Goal; // just here to determine if this be left side goal or right side goal

    // GAME OBJECTS
    GameManager gm;

    // COMPONENTS
    AudioSource sound;
    
    private void Start()
    {
        // GAME OBJECTS AND COMPONENTS
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sound = GetComponent<AudioSource>();
    }

    // Runs when there is collision 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HIT!"); // HARDER!!!

        // 
        if (other.gameObject.CompareTag("Ball")) // Probably not necessary here since the ball is the only thing moving though the X-axis, but eh, just bein sure
        {
            sound.Play(); // Ting sound effect
            if(isPlayer1Goal) // Checks if left side goal or right side goal
            {
                gm.PlayerScored(2); // Player 2 scored
            } else {
                gm.PlayerScored(1); // Player 1 scored
            }
        }
    }

}

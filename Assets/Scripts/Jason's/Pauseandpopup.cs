using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauseandpopup : MonoBehaviour
{
    // The key to press to unpause the game
    public KeyCode unpauseKey = KeyCode.P;

    // Whether the game is currently paused
    private bool isPaused = false;

    void Update()
    {
        // Check if the game is paused and if the unpause key is pressed
        if (isPaused && Input.GetKeyDown(unpauseKey))
        {
            UnpauseGame();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for trigger collision with specific object
        if (other.CompareTag("PauseTrigger"))
        {
            Debug.Log("Trigger detected with PauseTrigger. Pausing game.");
            PauseGame();
        }
    }

    void PauseGame()
    {
        // Set the timescale to 0 to pause the game
        Time.timeScale = 0;
        isPaused = true;
        Debug.Log("Game paused.");
    }

    void UnpauseGame()
    {
        // Set the timescale to 1 to resume the game
        Time.timeScale = 1;
        isPaused = false;
        Debug.Log("Game unpaused.");
    }
}

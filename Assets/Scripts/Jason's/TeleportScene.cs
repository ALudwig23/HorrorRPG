using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScene : MonoBehaviour
{
    public string sceneToLoad;

    // Ensure you have a Collider2D component (with Is Trigger checked) on your GameObject
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the player tag
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

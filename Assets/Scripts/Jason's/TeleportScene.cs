using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScene : MonoBehaviour
{
    public int LoadLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(LoadLevel, LoadSceneMode.Single);
        }
    }
}

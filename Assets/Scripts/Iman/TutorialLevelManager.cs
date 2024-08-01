using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public Door door; // Reference to the door script

    void Start()
    {
        if (GameState.isPuzzleCompleted)
        {
            door.Unlock();
            door.Open();
        }
    }
}


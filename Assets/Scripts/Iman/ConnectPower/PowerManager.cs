using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerManager : MonoBehaviour
{
    public GameObject CableHolder;
    public GameObject[] Cables;
    public Door door; // Reference to the door script

    [SerializeField]
    private int totalCables = 0;
    [SerializeField]
    private int correctedCables = 0;

    [SerializeField]
    private float holdDuration = 2f; // Duration the spacebar needs to be held in seconds
    private float holdTimer = 0f;
    private bool holdingSpace = false;

    void Start()
    {
        totalCables = CableHolder.transform.childCount;

        Cables = new GameObject[totalCables];

        for (int i = 0; i < Cables.Length; i++)
        {
            Cables[i] = CableHolder.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (correctedCables == totalCables && !holdingSpace)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                holdTimer += Time.deltaTime;
                if (holdTimer >= holdDuration)
                {
                    holdingSpace = true;
                    CompletePuzzle();
                }
            }
            else
            {
                holdTimer = 0f;
            }
        }
    }

    public void CorrectMove()
    {
        correctedCables += 1;

        Debug.Log("Correct move");

        if (correctedCables == totalCables)
        {
            Debug.Log("All cables placed correctly. Hold spacebar to complete.");
        }
    }

    public void WrongMove()
    {
        correctedCables -= 1;
    }

    private void CompletePuzzle()
    {
        Debug.Log("Puzzle completed! Loading next scene...");
        GameState.isPuzzleCompleted = true;
        SceneManager.LoadScene("TutorialLevel"); // Go back to the tutorial level
    }
}



using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public GameObject CableHolder;
    public GameObject[] Cables;
    public Door door; // Reference to the door script
    public CompleteButton completeButton; // Reference to the CompleteButton script

    [SerializeField]
    private int totalCables = 0;
    [SerializeField]
    private int correctedCables = 0;

    void Start()
    {
        totalCables = CableHolder.transform.childCount;
        Cables = new GameObject[totalCables];

        for (int i = 0; i < Cables.Length; i++)
        {
            Cables[i] = CableHolder.transform.GetChild(i).gameObject;
        }

        // Set the button listener
        if (completeButton != null)
        {
            completeButton.onComplete += OnCompleteButtonPressed;
            completeButton.gameObject.SetActive(false); // Hide initially
        }
    }

    void Update()
    {
        // Check if all cables are corrected and the button has not been held yet
        if (correctedCables == totalCables && !completeButton.gameObject.activeSelf)
        {
            // Show the complete button
            if (completeButton != null)
            {
                completeButton.gameObject.SetActive(true);
            }
        }
    }

    public void CorrectMove()
    {
        correctedCables += 1;

        Debug.Log("Correct move");

        if (correctedCables == totalCables)
        {
            Debug.Log("All cables placed correctly. Press the button to complete.");
        }
    }

    public void WrongMove()
    {
        correctedCables -= 1;
    }

    private void OnCompleteButtonPressed()
    {
        if (correctedCables == totalCables)
        {
            CompletePuzzle();
        }
    }

    public void CompletePuzzle()
    {
        Debug.Log("Puzzle completed! Loading next scene...");
        GameState.isPuzzleCompleted = true;
        SceneManager.LoadScene("TutorialLevel"); // Go back to the tutorial level
    }
}





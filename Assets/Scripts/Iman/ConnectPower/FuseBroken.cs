using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FuseBroken : MonoBehaviour
{
    public GameObject dialogueBox;    // Reference to the dialogue box panel
    public TMP_Text messageText;      // Reference to the TextMeshPro Text UI element
    public Button yesButton;          // Reference to the Yes button
    public Button noButton;           // Reference to the No button
    private bool isPlayerInRange = false;

    private void Start()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false); // Hide the dialogue box at the start
        }

        // Add listeners to the buttons
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowFixPrompt();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void ShowFixPrompt()
    {
        if (dialogueBox != null && messageText != null)
        {
            dialogueBox.SetActive(true);
            messageText.text = "You wanna fix it?";
        }
    }

    private void OnYesButtonClicked()
    {
        // Transition to the power cable puzzle scene
        SceneManager.LoadScene("PuzzleConnect");
    }

    private void OnNoButtonClicked()
    {
        // Hide the dialogue box
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }
}


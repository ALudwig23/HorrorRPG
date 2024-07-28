using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text messageText;       // Reference to the TextMeshPro Text UI element
    public GameObject dialogueBox;     // Reference to the dialogue box panel
    public TypewriterEffect typewriterEffect; // Reference to the TypewriterEffect script

    void Start()
    {
        if (messageText != null)
        {
            messageText.text = "";  // Initialize the text to be empty
        }

        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false); // Hide the dialogue box at the start
        }

        if (typewriterEffect == null)
        {
            typewriterEffect = GetComponent<TypewriterEffect>();
        }
    }

    public void ShowMessage(string message, float duration)
    {
        if (messageText != null && dialogueBox != null && typewriterEffect != null)
        {
            StartCoroutine(DisplayMessage(message, duration));
        }
    }

    private IEnumerator DisplayMessage(string message, float duration)
    {
        dialogueBox.SetActive(true);
        yield return StartCoroutine(typewriterEffect.Run(message, messageText)); // Use typewriter effect
        yield return new WaitForSeconds(duration);
        messageText.text = "";
        dialogueBox.SetActive(false);
    }

    public void ShowPrompt(string prompt)
    {
        if (messageText != null && dialogueBox != null)
        {
            dialogueBox.SetActive(true);
            messageText.text = prompt;
        }
    }

    public void HidePrompt()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }
}
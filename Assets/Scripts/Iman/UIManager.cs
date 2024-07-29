using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text messageText;       // Reference to the TextMeshPro Text UI element
    public GameObject dialogueBox;     // Reference to the dialogue box panel
    public Button yesButton;           // Reference to the Yes button
    public Button noButton;            // Reference to the No button
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

        // Hide buttons initially
        if (yesButton != null) yesButton.gameObject.SetActive(false);
        if (noButton != null) noButton.gameObject.SetActive(false);
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
            yesButton.gameObject.SetActive(true); // Show the Yes button
            noButton.gameObject.SetActive(true);  // Show the No button
        }
    }

    public void HidePrompt()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }

    public void HideButtons()
    {
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }
}

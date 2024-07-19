using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text messageText;   // Reference to the Text UI element

    void Start()
    {
        if (messageText != null)
        {
            messageText.text = "";  // Initialize the text to be empty
        }
    }

    public void ShowMessage(string message, float duration)
    {
        if (messageText != null)
        {
            StartCoroutine(DisplayMessage(message, duration));
        }
    }

    private IEnumerator DisplayMessage(string message, float duration)
    {
        messageText.text = message;
        yield return new WaitForSeconds(duration);
        messageText.text = "";
    }
}

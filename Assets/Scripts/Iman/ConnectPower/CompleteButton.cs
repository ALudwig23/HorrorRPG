using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CompleteButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image progressBar; // Reference to the ProgressBar image
    public float holdDuration = 3f; // Time required to hold the button in seconds
    public delegate void CompleteAction();
    public event CompleteAction onComplete; // Event to notify when the task is completed

    private float holdTimer = 0f;
    private bool isHolding = false;
    private bool taskCompleted = false;

    private void Start()
    {
        if (progressBar != null)
        {
            progressBar.fillAmount = 0f; // Initialize the progress bar to empty
        }

        Debug.Log("CompleteButton started. Initialized progress bar and variables.");
    }

    private void Update()
    {
        if (isHolding && !taskCompleted)
        {
            holdTimer += Time.deltaTime;
            float progress = holdTimer / holdDuration;
            progressBar.fillAmount = progress;

            Debug.Log($"Holding... holdTimer: {holdTimer}, Progress: {progress}");

            // Ensure the task completes only when the progress is fully filled
            if (progress >= 1f)
            {
                Debug.Log("Progress bar filled. Completing task.");
                CompleteTask();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!taskCompleted)
        {
            isHolding = true;
            Debug.Log("Pointer down. Starting hold.");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        Debug.Log($"Pointer up. holdTimer: {holdTimer}");

        // If the hold timer hasn't reached the required duration, reset the progress
        if (holdTimer < holdDuration)
        {
            Debug.Log("Hold duration not met. Resetting progress.");
            ResetProgress();
        }
    }

    private void CompleteTask()
    {
        // Ensure this only runs once
        if (taskCompleted) return;

        taskCompleted = true;
        Debug.Log("Task completed!");

        // Disable button interaction to prevent further triggering
        GetComponent<Button>().interactable = false;

        // Notify PowerManager to complete the puzzle
        if (onComplete != null)
        {
            onComplete.Invoke();
        }
    }

    private void ResetProgress()
    {
        holdTimer = 0f;
        if (progressBar != null)
        {
            progressBar.fillAmount = 0f;
        }
        Debug.Log("Progress reset.");
    }
}



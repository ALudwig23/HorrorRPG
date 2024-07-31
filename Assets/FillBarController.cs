using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBarController : MonoBehaviour
{
    public SpriteRenderer fillBar;   // The fill bar's SpriteRenderer
    public float fillSpeed = 0.2f;   // Speed at which the bar fills
    public float drainSpeed = 0.1f;  // Speed at which the bar drains
    private bool isFilling = false;  // Is the bar currently filling?
    private bool isFilled = false;   // Has the bar been completely filled?

    private Vector3 initialScale;    // Initial scale of the fill bar

    void Start()
    {
        initialScale = fillBar.transform.localScale;  // Store the initial scale of the fill bar
        fillBar.transform.localScale = new Vector3(0f, initialScale.y, initialScale.z);  // Start with the fill bar empty
    }

    void Update()
    {
        if (isFilling && !isFilled)  // If the bar is filling and not yet filled completely
        {
            Vector3 newScale = fillBar.transform.localScale;
            newScale.x += fillSpeed * Time.deltaTime;  // Increase the scale of the fill bar
            fillBar.transform.localScale = new Vector3(Mathf.Clamp(newScale.x, 0f, initialScale.x), initialScale.y, initialScale.z);

            if (fillBar.transform.localScale.x >= initialScale.x)  // If the fill bar is completely filled
            {
                fillBar.transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);  // Cap the fill bar at full size
                isFilled = true;  // Mark the bar as filled
                isFilling = false;  // Stop the filling process
            }
        }
        else if (!isFilling && !isFilled)  // If the bar is not filling and is not yet filled completely
        {
            Vector3 newScale = fillBar.transform.localScale;
            newScale.x -= drainSpeed * Time.deltaTime;  // Decrease the scale of the fill bar
            fillBar.transform.localScale = new Vector3(Mathf.Clamp(newScale.x, 0f, initialScale.x), initialScale.y, initialScale.z);

            if (fillBar.transform.localScale.x <= 0f)  // If the fill bar is completely drained
            {
                fillBar.transform.localScale = new Vector3(0f, initialScale.y, initialScale.z);  // Cap the fill bar at empty size
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse button pressed");
        GetComponent<SpriteRenderer>().color = Color.red; // Change colour on press
        if (!isFilled)
        {
            isFilling = true;  // Start filling the bar when the mouse button is pressed
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse button released");
        GetComponent<SpriteRenderer>().color = Color.white; // Revert color on release
        if (!isFilled)
        {
            isFilling = false;  // Stop filling the bar when the mouse button is released
        }
    }
}

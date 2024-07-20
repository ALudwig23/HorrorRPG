using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullBox : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private bool isBeingDragged = false;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 offset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Ensure the box does not fall down
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; // Initially freeze position
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isBeingDragged = true;
                offset = (Vector2)transform.position - (Vector2)player.transform.position;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Allow movement
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isBeingDragged = false;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; // Freeze the box in place
            }
        }

        if (isBeingDragged)
        {
            // Smoothly follow the player position with an offset
            Vector2 targetPosition = (Vector2)player.transform.position + offset;
            rb.MovePosition(Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 10f)); // Adjust the speed factor as needed
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            player = null;
            if (isBeingDragged)
            {
                isBeingDragged = false;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; // Freeze the box in place
            }
        }
    }
}

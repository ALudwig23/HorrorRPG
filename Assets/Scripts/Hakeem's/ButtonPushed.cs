using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonPushed : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool isPressed = false;
    public GameObject popUpText;
    private bool isPlayerNearby = false;
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone");
            //isPlayerNearby = true;
            //Debug.Log("Player is nearby.");

            if (Input.GetKeyDown(KeyCode.E))
            {
                isPressed = true;
            }

        }

    }
}

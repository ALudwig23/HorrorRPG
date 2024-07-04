using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // The chasing target
    public float normalChaseRadius = 5.0f; // Radius within which the enemy starts chasing
    public float torchChaseRadius = 10.0f; // Chase radius when player is holding torchlight
    public float moveSpeed = 2.0f; // Enemy's speed

    private bool isChasing = false; // is it still chasing?

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Determine current chase radius based on torchlight state
            float chaseRadius = TorchlightState.instance.GetTorchlightState() ? torchChaseRadius : normalChaseRadius;

            // Set destination to player's position if within chase radius
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= chaseRadius)
            {
                isChasing = true;
                ChasePlayer();
            }
            else
            {
                isChasing = false;
                // Optionally, you can add behavior for when the enemy is not chasing
            }
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        // chase radius in the scene view for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, normalChaseRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, torchChaseRadius);
    }
}


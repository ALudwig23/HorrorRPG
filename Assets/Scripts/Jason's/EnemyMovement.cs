using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // the chasing target
    public float chaseRadius = 5.0f; // Radius within which the enemy starts chasing
    public float moveSpeed = 2.0f; // Enemy's speed

    private bool isChasing = false; // is it still chasing?

    void Update() // what it will be doing if it is chasing and not chasing
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer() //script that chases the player
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        // chase radius in the scene view for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}

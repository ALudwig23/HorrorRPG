using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player; // The chasing target
    [SerializeField] private float chaseRadius = 5.0f; // Radius within which the enemy starts chasing
    [SerializeField] private float moveSpeed = 2.0f; // Enemy's speed

    [SerializeField] private bool isChasing = false; // is it still chasing?

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(collision.gameObject); to destroy player
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base class that all AI in the game will inherit from
/// this includes towers and enemies
/// </summary>
public class AIController : MonoBehaviour
{
    private Transform player;
    public float movementSpeed = 5.0f;
    public float attackDistance = 3.0f; // Set the distance at which the AI attacks the player
    public float attackCooldown = 2.0f; // Set a cooldown period for attacks
    private float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if the player is within attack distance
            if (distanceToPlayer <= attackDistance)
            {
                // Check if enough time has passed since the last attack
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    // Stop moving
                    StopMoving();

                    // Do something to the player (e.g., damage, animation, etc.)
                    AttackPlayer();

                    // Update the last attack time
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                // Move towards the player if not within attack distance
                MoveTowardsPlayer();
            }
        }
    }

    // Function to stop AI movement
    void StopMoving()
    {
        // Stop AI movement logic (e.g., disable Rigidbody forces)
    }

    // Function to simulate an attack on the player
    void AttackPlayer()
    {
        // Perform your attack logic here (e.g., reduce player health)
        Debug.Log("Attacking the player!");
    }

    // Function to move towards the player
    void MoveTowardsPlayer()
    {
        // Move AI towards the player (e.g., enable Rigidbody forces)
        transform.LookAt(player);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }
}

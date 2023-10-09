using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// base class that all AI in the game will inherit from
/// this includes towerTransform and enemies
/// </summary>
public class AIController : MonoBehaviour
{
    #region Targets
    [Header ("Enemy Controller")]
    [Header("Move Towards")]
    public PlayerController player;
    public Transform playerTransform;
    public Transform[] towerTransform;
    public Transform playerBase;
    #endregion

    #region Variables
    [Header ("Variables")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float attackDistance = 1.0f;
    [SerializeField] private float attackCooldown = 2.0f;
    [SerializeField] private float baseDetectionRange = 10.0f;
    [SerializeField] private float playerDetectionRange = 5.0f;

    [SerializeField] private bool targetTowers = true; //This bool controls Whether the enemy attacks a tower or the base.

    private Animator animator;
    private bool isMoving;
    private Vector3 lastPosition;
    private float lastAttackTime;
    private Transform currentTarget;
    #endregion

    #region AIState Enum
    private enum AIState
    {
        Idle,
        Moving,
        Attacking
    }

    private AIState aiState = AIState.Idle;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.GetInstance().GetPlayerController();
        playerTransform = player.transform;
        animator = GetComponentInChildren<Animator>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();

        UpdateAIState();

        UpdateAnimation();

        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToTarget <= attackDistance)
            {
                StopMoving();
                AttackTarget(currentTarget);

            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    #region Main Behavior
    private void StopMoving()
    {
        //The function doesn't do anything, but just stops it's movement.
    }

    private void FindClosestTarget() //This entire function is to calculate which is the closest target that is assigned to.
    {
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        //This if-else statement is to check whether the target towers check mark in the inspector in true or false.
        //If true it targets the nearest tower it's closest to.
        if (targetTowers)
        {
            foreach (Transform target in towerTransform)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }
        }
        //If false it goes straight to the player's base.
        else
        {
            float distanceToBase = Vector3.Distance(transform.position, playerBase.position);

            if (distanceToBase <= baseDetectionRange) ;
            {
                closestTarget = playerBase;
            }
        }

        //This is just to chase after the player when they are at a certain range of the enemy.
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < playerDetectionRange)
        {
            closestTarget = playerTransform;
        }

        currentTarget = closestTarget;
    }

    private void AttackTarget(Transform target)
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            IDamageable damageInterface = player.gameObject.GetComponent<IDamageable>();
            if (damageInterface != null)
            {
                player.Damage(123.0f);
            }
            Debug.Log("Attack");
        }

        lastAttackTime = Time.time;
    }

    private void MoveTowardsPlayer()
    {
        Vector3 distanceToTarget = currentTarget.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(distanceToTarget.x, 0f, distanceToTarget.z));
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }
    #endregion

    #region AI Animation Enum
    private void UpdateAIState()
    {
        // Check if the AI is within the attack distance
        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToTarget <= attackDistance)
            {
                // Set the AI state to attacking
                aiState = AIState.Attacking;
            }
            else
            {
                // Set the AI state to moving
                aiState = AIState.Moving;
            }
        }
        else
        {
            // If there's no current target, set the AI state to idle
            aiState = AIState.Idle;
        }
    }

    private void UpdateAnimation()
    {
        // Check the AI state and update animations accordingly
        switch (aiState)
        {
            case AIState.Idle:
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", false);
                break;
            case AIState.Moving:
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
                break;
            case AIState.Attacking:
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", true);
                break;
        }
    }
    #endregion
}

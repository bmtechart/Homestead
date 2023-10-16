using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// base class that all AI in the game will inherit from
/// this includes towerTransform and enemies
/// </summary>

public enum TargetPriortiy
{
    Homestead,
    Tower
}

[RequireComponent(typeof(HealthBehaviour))]
[RequireComponent(typeof(AIMovementBehaviour))]
public class AIController : MonoBehaviour, IDamageable
{
    public UnityEvent<GameObject> m_OnTargetFound; //when a viable target is found, broadcast this message


    /// <summary>
    /// -make sure that the AI Controller is the parent class and its children inherit from that (a lot of private variables will need to become protected)
    /// -AI Send out events giving some information like their position, who they are tracking, etc.
    /// </summary>
    #region Targets
    [Header ("Enemy Controller")]
    [Header("Move Towards")]
    protected PlayerController player;
    protected Transform playerTransform;
    [SerializeField] protected Transform[] towerTransform;
    [SerializeField] protected Transform playerBase;
    #endregion

    #region Variables
    [Header ("Variables")]
    [SerializeField] protected float movementSpeed = 5.0f;
    [SerializeField] protected float attackDistance = 1.0f;
    [SerializeField] protected float attackCooldown = 2.0f;
    [SerializeField] protected float baseDetectionRange = 10.0f;
    [SerializeField] protected float playerDetectionRange = 5.0f;
    [SerializeField] protected float enemyDamage = 123.0f;

    [SerializeField] protected bool targetTowers = true; //This bool controls Whether the enemy attacks a tower or the base.

    protected Animator animator;
    protected bool isMoving;
    protected Vector3 lastPosition;
    protected float lastAttackTime;
    protected Transform currentTarget;
    #endregion

    private HealthBehaviour healthBehaviour;
    private Rigidbody rb;

    #region AIState Enum
    protected enum AIState
    {
        Idle,
        Moving,
        Attacking
    }

    protected AIState aiState = AIState.Idle;
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameManager.GetInstance().GetPlayerController(); //returns player controller
        playerTransform = player.transform; //store player position/rotation
        animator = GetComponentInChildren<Animator>();
        lastPosition = transform.position;

        healthBehaviour = GetComponent<HealthBehaviour>();
        rb = GetComponent<Rigidbody>();

        //start by finding the closest appropriate target
        //FindClosestTarget();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FindClosestTarget();
        UpdateAIState();
        UpdateAnimation();

        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToTarget <= attackDistance)
            {
                AttackTarget(currentTarget);

            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    #region Main Behavior

    protected virtual void FindClosestTarget()
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

            if (distanceToBase <= baseDetectionRange);
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
            IDamageable damageInterface = target.gameObject.GetComponent<IDamageable>();
            if (damageInterface != null)
            {
                player.Damage(gameObject, enemyDamage);
            }
            Debug.Log("Attack");
        }

        lastAttackTime = Time.time;
    }

    protected virtual void MoveTowardsPlayer()
    {
        if(player != null)
        {
            Vector3 distanceToTarget = currentTarget.position - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(distanceToTarget.x, 0f, distanceToTarget.z));
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region AI Animation Enum
    protected virtual void UpdateAIState()
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

    protected virtual void UpdateAnimation()
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

    public void Damage(GameObject source, float damageAmount)
    {
        //push enemy away from object that damaged them
        if(rb)
        {
            rb.AddForce((transform.position - source.transform.position) * 10);
        }
        //play damage sound
        //damage unit
        healthBehaviour.Damage(damageAmount);
    }

    public void OnDeath()
    {
        //play death animation
        //play death sound
        Destroy(gameObject);
    }
}

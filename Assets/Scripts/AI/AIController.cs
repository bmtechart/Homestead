using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base class that all AI in the game will inherit from
/// this includes towerTransform and enemies
/// </summary>
[RequireComponent(typeof(AIMovementBehaviour))]
[RequireComponent(typeof(AITargetAcquisitionBehaviour))]
[RequireComponent(typeof(HealthBehaviour))]
[RequireComponent(typeof(AIAnimationController))]
public class AIController : MonoBehaviour, IDamageable
{
    #region Variables
    [SerializeField] private GameObject aiTarget; 
    #endregion

    #region Components
    private AIMovementBehaviour movementBehaviour;
    private AITargetAcquisitionBehaviour targetAcquisitionBehaviour;
    private HealthBehaviour healthBehaviour;
    private AIAnimationController animationController;
    #endregion

    #region Runtime Callbacks
    protected virtual void Start()
    {
        //get component references
        movementBehaviour = GetComponent<AIMovementBehaviour>();
        targetAcquisitionBehaviour = GetComponent<AITargetAcquisitionBehaviour>();  
        healthBehaviour = GetComponent<HealthBehaviour>();
        animationController = GetComponent<AIAnimationController>();

        //bind events


        //start behaviour by finding target
        targetAcquisitionBehaviour.FindTarget();
    }

    protected virtual void Update()
    {

    }

    public void OnTargetFound(GameObject target) 
    {
        Debug.Log("new target found: " + target.name);
        aiTarget = target;
    }

    #endregion

    #region Damage & Death
    public void Damage(GameObject source, float damageAmount)
    {
        if (!healthBehaviour) return;
        healthBehaviour.Damage(damageAmount);
    }

    public void OnDeath()
    {
        //disable all behaviours
        movementBehaviour.enabled = false;
        targetAcquisitionBehaviour.enabled = false;

    }
    #endregion

    /*
    /// <summary>
    /// -make sure that the AI Controller is the parent class and its children inherit from that (a lot of private variables will need to become protected)
    /// -AI Send out events giving some information like their position, who they are tracking, etc.
    /// </summary>
    #region Targets
    [Header ("Enemy Controller")]
    [Header("Move Towards")]
    protected PlayerController player;
    protected Transform playerTransform;
    //protected BuildingController building;
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

    #region AIState Enum
    protected enum AIState
    {
        Idle,
        Moving,
        Attacking,
        Death
    }

    protected AIState aiState = AIState.Idle;
    #endregion


    public WaveManager waveManager;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        waveManager = GetComponentInParent<WaveManager>();

        player = GameManager.GetInstance().GetPlayerController();
        playerTransform = player.transform;
        //building = GameManager.GetInstance().GetBuildingController();
        //towerTransform = building.transform;
        animator = GetComponentInChildren<Animator>();
        lastPosition = transform.position;
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
    protected virtual void StopMoving()
    {
        //The function doesn't do anything, but just stops it's movement.
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        animator.SetBool("IsMoving", false);
    }

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

            if (distanceToBase <= baseDetectionRange)
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

        if(aiState == AIState.Idle)
        {
            StopMoving();
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
                animator.SetBool("IsDead", false);
                break;
            case AIState.Moving:
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsDead", false);
                break;
            case AIState.Attacking:
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", true);
                animator.SetBool("IsDead", false);
                break;
            case AIState.Death:
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsDead", true);
                break;
        }
    }

    public void Damage(GameObject source, float damageAmount)
    {
        HealthBehaviour hb = GetComponent<HealthBehaviour>();
        hb.Damage(damageAmount);
    }
    #endregion
    */
}

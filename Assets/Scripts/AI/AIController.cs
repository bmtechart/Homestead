using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// base class that all AI in the game will inherit from
/// this includes towerTransform and enemies
/// </summary>
public class AIController : MonoBehaviour
{
    #region Variables
    [Header ("Enemy Controller")]
    [Header("Move Towards")]
    public PlayerController player;
    public Transform playerTransform;
    public Transform[] towerTransform;
    public Transform playerBase;

    [Header ("Variable")]
    public float movementSpeed = 5.0f;
    public float attackDistance = 1.0f;
    public float attackCooldown = 2.0f;
    public float baseDetectionRange = 10.0f;
    public float playerDetectionRange = 5.0f;

    [SerializeField] public bool targetTowers = true; //This bool controls Whether the enemy attacks a tower or the base.
    
    /*[Header ("Animation")]
    public UnityEvent AttackStartEvent;
    public UnityEvent AttackStopEvent;
    public UnityEvent StartWalkEvent;
    public UnityEvent StopWalkEvent;*/

    //private Animator animator;
    //private eventDispatcher = GetComponent<AIAnimationEventDispatcher>();

    private float lastAttackTime;
    private Transform currentTarget;

    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.GetInstance().GetPlayerController();
        playerTransform = player.transform;

        //animator.GetComponent<Animator>();
        //eventDispatcher = GetComponent<AIAnimationEventDispatcher>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();

        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToTarget <= attackDistance)
            {
                StopMoving();
                AttackTarget(currentTarget);

                //AttackStart();

                //StopMoving();
            }
            else
            {
                MoveTowardsPlayer();

                //StartWalk();
            }
        }
        /*else
        {
            AttackStop();

            StartWalk();
        }*/
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

    #region Animation
    /*public void AttackStart()
    {
        if (AttackStartEvent != null)
        {
            AttackStartEvent?.Invoke();
        }
    }

    public void AttackStop()
    {
        if (AttackStopEvent != null)
        {
            AttackStopEvent?.Invoke();
        }
    }

    public void StartWalk()
    {
        if (StartWalkEvent != null)
        {
            StartWalkEvent?.Invoke();
        }
    }

    public void StopWalk()
    {
        if (StopWalkEvent != null)
        {
            StopWalkEvent?.Invoke();
        }
    }*/
    #endregion
}

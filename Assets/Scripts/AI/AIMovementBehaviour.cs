using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovementBehaviour : MonoBehaviour
{

    #region Event Dispatchers
    public UnityEvent m_OnPathInterrupted;
    public UnityEvent m_OnStartMoving;
    public UnityEvent m_OnDestinationReached;
    #endregion

    private NavMeshAgent agent;
    private NavMeshPath path;


    [SerializeField] private GameObject followTarget;

    public Transform TargetTransform
    {
        get {  return _targetTransform; }
        set 
        {
            _targetTransform = value;
            agent.isStopped = false;
            CalculateNewPath();
        }
    }

    private Transform _targetTransform;

    Vector3 followTargetPosition;

    [Tooltip("If following a moving target, only reset the destination if the target deviates from its initial position by this amount.")]
    [SerializeField] private float targetReaquisitionDistance = 10.0f;
    [Tooltip("Maximum distance of raycast to potential target. If the raycast fails, the unit will not move to the target. Prevents unit from calculating absurdly long paths. Set to 0 for infinite distance.")]
    [SerializeField] private float maxDistanceToTarget = 10000.0f;
    [SerializeField] private float stoppingDistance = 5.0f;
    [SerializeField] private bool drawDebug;
    [SerializeField] GameObject moveRayOrigin;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
        path = new NavMeshPath();
        


    }

    void OnGameOver(bool playerVictory)
    {
        if (playerVictory) return;
        StopMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (!followTarget) return;
        if(Vector3.Distance(followTargetPosition, followTarget.transform.position) > targetReaquisitionDistance)
        {
            SetDestination();
        }
        
        if(Vector3.Distance(followTargetPosition, transform.position) <= stoppingDistance)
        {
            m_OnDestinationReached?.Invoke();
        }
        if (drawDebug)
        {
            Vector3 rayOffset = new Vector3(0, agent.height / 2.0f, 0);
            Vector3 rayDirection = (followTargetPosition - transform.position) * Vector3.Distance(followTargetPosition, transform.position);
            Debug.DrawRay(transform.position+rayOffset, rayDirection+rayOffset, Color.red, 0.0f, true);
        }
    }

    public void OnTargetAcquired(GameObject target)
    {
        if (!target) return;
        followTarget = target;

        SetDestination(); //only set new destination if it can be reached
    }

    public bool SetDestination()
    {
        //if there is no target, fail
        if (!followTarget) return false;


        //instead of moving to root position of target object, 
        //use raycast to move to closest edge of collider of target
        Collider _collider = followTarget.GetComponent<CapsuleCollider>();


        //Vector3 rayOffset = new Vector3(0, agent.height / 2.0f, 0); //draw ray from center of capsule

        Ray ray = new Ray(transform.position, (followTarget.transform.position-transform.position));
        RaycastHit hit;
        float rayLength = maxDistanceToTarget;

        if (maxDistanceToTarget == 0) rayLength = Mathf.Infinity;


        if(_collider.Raycast(ray, out hit, rayLength))
        {
            if (agent) agent.destination = hit.point; //update nav mesh agent
            
            followTargetPosition = followTarget.transform.position; //cache position of follow target. Used in update to determine if we need to reset path for moving targets.
            m_OnStartMoving?.Invoke();
            Debug.Log("started moving again!");
            return true;
        } 

        return false;
    }

    public bool CalculateNewPath()
    {
        if(!followTarget) return false;
        if (!_targetTransform) return false;
        agent.CalculatePath(followTargetPosition, path);
        Debug.Log("New path calculated");

        if(path.status != NavMeshPathStatus.PathComplete)  //if we cannot path to target location
        {
            //return false so we can move on to next possible target
            m_OnPathInterrupted?.Invoke();
            return false;
        }
        else
        {
            return true;
        }
    }

    public void StopMovement()
    {
        agent.isStopped = true;
    }
}

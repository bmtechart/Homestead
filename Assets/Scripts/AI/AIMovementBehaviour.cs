using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovementBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private NavMeshPath path;

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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDestination(Vector3 destination)
    {
        if (agent) agent.destination = destination;

    }

    public bool CalculateNewPath()
    {
        agent.CalculatePath(_targetTransform.position, path);
        Debug.Log("New path calculated");

        if(path.status != NavMeshPathStatus.PathComplete)  //if we cannot path to target location
        {
            //return false so we can move on to next possible target
            return false;
        }
        else
        {
            //set destination to target position
            agent.destination = _targetTransform.position;
            return true;
        }
    }


    public void StopMovement()
    {
        agent.isStopped = true;
    }
}

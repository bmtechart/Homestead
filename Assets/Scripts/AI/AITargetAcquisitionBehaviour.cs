using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Runtime.CompilerServices;

public enum AITargetPriority
{
    Player,
    Tower,
    Homestead
}

public class AITargetAcquisitionBehaviour : MonoBehaviour
{
    [SerializeField] private AITargetPriority[] priority;
    [SerializeField] private GameObject target;

    public UnityEvent<GameObject> m_OnTargetAcquired;
    public UnityEvent m_OnTargetLost;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            Debug.Log("lost follow target!");
            m_OnTargetLost.Invoke();
        }
    }

    public void FindTarget()
    {
        List<GameObject> targets = GetAvailableTargets();
        target = GetClosestGameObject(targets);
        if (!target) return;
        m_OnTargetAcquired?.Invoke(target);
    }

    private List<GameObject> GetAvailableTargets()
    {
        List<GameObject> targets = new List<GameObject>();

        foreach (AITargetPriority tp in priority)
        {
            switch(tp)
            {
                default:
                    break;
                case AITargetPriority.Player:
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if(player) targets.Add(player);
                    break;
                case AITargetPriority.Tower:
                    targets.AddRange(GameObject.FindGameObjectsWithTag("tower"));
                    break;
                case AITargetPriority.Homestead:
                    targets.Add(GameObject.FindGameObjectWithTag("homestead"));
                    break;
            }
        }

        return targets;
    }

    private GameObject GetClosestGameObject(List<GameObject> objects)
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        if (objects.Count == 0) return null;

        foreach(GameObject obj in objects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }

}

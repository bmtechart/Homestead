using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIAttackBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject attackTarget;
    [SerializeField] private float attackRange;

    public UnityEvent m_OnEnterAttackRange;
    public UnityEvent m_OnLeaveAttackRange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if no valid target to attack, this behaviour does nothing
        if (!attackTarget) return;

        if(Vector3.Distance(transform.position, attackTarget.transform.position)<=attackRange)
        {

        }

    }

    public void StartAttack()
    {
        //broadcast start attac
    }

    //expression bodied members are cool!
    public void OnTargetAcquired(GameObject target) => attackTarget = target;
}

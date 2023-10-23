using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIAttackBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject attackTarget;
    [SerializeField] private float attackRange;
    [SerializeField] private bool isAttacking = false;

    public UnityEvent m_OnEnterAttackRange;
    public UnityEvent m_OnLeaveAttackRange;
    public UnityEvent m_OnAttack;

    [SerializeField] private float attackDelay = 3.0f;
    [SerializeField] private float attackDamage;
    // Start is called before the first frame update
    protected virtual void Start()
    {

    }



    // Update is called once per frame
    protected virtual void Update()
    {
        //if no valid target to attack, this behaviour does nothing
        if (!attackTarget) return;

        //if we are in range of our target
        if(Vector3.Distance(transform.position, attackTarget.transform.position)<=attackRange)
        {
            if (isAttacking) return;
            isAttacking = true;
            m_OnEnterAttackRange.Invoke();
        }


        if(Vector3.Distance(transform.position, attackTarget.transform.position) >= attackRange)
        {
            m_OnLeaveAttackRange.Invoke();
            isAttacking = false;
        }

        
    }

    public virtual void OnStartAttack()
    {

    }

    public virtual void OnStopAttack()
    {

    }

    //expression bodied members are cool!
    public virtual void OnTargetAcquired(GameObject target) => attackTarget = target;
    public void OnTargetLost() => isAttacking = false;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIAttackBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject attackTarget;
    [SerializeField] private float attackRange;
    public bool isAttacking = false;

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
        if(CheckRange())
        {
            if (isAttacking) return;
            isAttacking = true;
            Debug.Log("started attacking");
            m_OnEnterAttackRange.Invoke();
        }

        if(!CheckRange())
        {
            m_OnLeaveAttackRange.Invoke();
            isAttacking = false;
        }   
    }

    protected bool CheckRange()
    {
        if (!attackTarget) return false;
        Ray ray = new Ray(transform.position, (attackTarget.transform.position - transform.position));
        RaycastHit hit;
        float rayLength = Mathf.Infinity;
        Collider _collider = attackTarget.GetComponent<CapsuleCollider>();
        if (!_collider) _collider = attackTarget.GetComponent<BoxCollider>();

        if (_collider.Raycast(ray, out hit, rayLength))
        {
            if(hit.distance <= attackRange)
            {
                return true;
            }
        }
        return false;
    }

    public virtual void OnStartAttack()
    {

    }

    public virtual void OnStopAttack()
    {

    }

    //expression bodied members are cool!
    public virtual void OnTargetAcquired(GameObject target)
    {
        attackTarget = target;
    }
    public void OnTargetLost() => isAttacking = false;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEventDispatcher : MonoBehaviour
{
    public UnityEvent m_OnAttackStart;
    public UnityEvent m_OnAttackEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackHit()
    {

    }

    public void EnableHit()
    {
        m_OnAttackStart?.Invoke();
    }

    public void DisableHit()
    {
        m_OnAttackEnd?.Invoke();
    }
}

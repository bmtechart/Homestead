using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationEventDispatcher : MonoBehaviour
{
    public UnityEvent m_OnHitStart;
    public UnityEvent m_OnHitEnd;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitStart()
    {
        m_OnHitStart?.Invoke();
    }

    public void HitEnd()
    {
        m_OnHitEnd?.Invoke();
    }

    public void DeathAnimationComplete()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationEventDispatcher : MonoBehaviour
{
    public UnityEvent m_OnHitStart;
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
        Debug.Log("enemy attack start!");
    }

    public void HitEnd()
    {
        Debug.Log("enemy attack end!");
    }
}

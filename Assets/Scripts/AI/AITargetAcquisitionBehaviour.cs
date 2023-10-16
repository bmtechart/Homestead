using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class AITargetAcquisitionBehaviour : MonoBehaviour
{
    public UnityEvent m_OnTargetAcquired;
    public UnityEvent m_OnTargetLost;
    // Start is called before the first frame update
    void Start()
    {
        m_OnTargetAcquired?.Invoke();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

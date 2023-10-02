using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject defaultUI;
    
    public override void Awake()
    {
        base.Awake();
    }
}

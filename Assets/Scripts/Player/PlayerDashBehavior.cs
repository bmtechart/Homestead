using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDashBehavior : MonoBehaviour
{

    #region Variables
    Vector3 _movementInput;
    #endregion

    #region Events
    
    #endregion

    #region Runtime Callbacks
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Methods
    public void GetMovementInput(Vector2 input)
    {
        _movementInput = new Vector3(input.x, 0.0f, input.y);
    }
    #endregion
}

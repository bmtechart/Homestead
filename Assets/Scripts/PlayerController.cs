using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    #region Components
    private PlayerInput playerInput;
    #endregion

    #region Events
    public UnityEvent<Vector2> m_OnPlayerMoveStart;
    public UnityEvent<Vector2> m_OnPlayerMoveCancel;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMove(InputAction.CallbackContext ctx)
    {
        //get movement vector from input
        Vector2 movementVector = ctx.ReadValue<Vector2>();
        Debug.Log(ctx.phase);

        //invoke event broadcasting updated input value
        if (ctx.phase == InputActionPhase.Performed) { m_OnPlayerMoveStart?.Invoke(movementVector); }
        if (ctx.phase == InputActionPhase.Canceled) { m_OnPlayerMoveCancel?.Invoke(movementVector); }
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {

    }

    public void OnUseItem(InputAction.CallbackContext ctx) 
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// The player controller class receives player input
/// and generates events events based on that input.
/// Events generated by this class should be human readable
/// and relevant to gameplay. 
/// For example, a player input component tells us the mouse has been clicked.
/// This class tells us that a mouse click means the player should attack, and
/// creates relevant callbacks based on player input.
/// </summary>

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(HealthBehaviour))]
public class PlayerController : MonoBehaviour, IDamageable
{
    #region Components
    private PlayerInput playerInput;
    private HealthBehaviour healthBehaviour;    
    #endregion

    #region Events

    
    [Header("Event Dispatchers")]
    [Header("Movement")]
    public UnityEvent<Vector2> m_OnPlayerMoveStart;
    public UnityEvent<Vector2> m_OnPlayerMoveCancel;


    [Header("Attacking")]
    public UnityEvent m_OnPlayerAttackStart;
    public UnityEvent m_OnPlayerAttackCancel;

    [Header("Building")]
    public UnityEvent m_OnPlayerEnterBuildMode;
    public UnityEvent m_OnPlayerExitBuildMode;
    public UnityEvent m_OnPlayerBuildStructure;
    public UnityEvent m_OnPlayerSwapBuilding;

    [Header("Damage")]
    public UnityEvent<float> m_OnPlayerDamage;
    #endregion



    #region Runtime Callbacks
    // Start is called before the first frame update
    void Start()
    {
        //initialize player input
        playerInput = GetComponent<PlayerInput>();
        SetInputContext("Default");

        //bind death to game over
        healthBehaviour = GetComponent<HealthBehaviour>();
        healthBehaviour.OnDeath.AddListener(GameManager.GetInstance().GameOver);
    }

    #endregion

    #region Input Callbacks

    #region Default
    public void OnMove(InputAction.CallbackContext ctx)
    {
        //get movement vector from input
        Vector2 movementVector = ctx.ReadValue<Vector2>();

        //invoke event broadcasting updated input value
        if (ctx.phase == InputActionPhase.Performed) { m_OnPlayerMoveStart?.Invoke(movementVector); }
        if (ctx.phase == InputActionPhase.Canceled) { m_OnPlayerMoveCancel?.Invoke(movementVector); }
    }

    public void OnEnterBuildMode(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Started) 
        {
            m_OnPlayerEnterBuildMode?.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started) { m_OnPlayerAttackStart?.Invoke(); }
        if (ctx.phase == InputActionPhase.Canceled) { m_OnPlayerAttackCancel?.Invoke(); }
    }

    #endregion

    #region Build Mode
    public void OnExitBuildMode(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Started)
        {
            m_OnPlayerExitBuildMode?.Invoke();
        }
    }

    public void OnBuild(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Started)
        {
            m_OnPlayerBuildStructure?.Invoke();
        }
    }

    public void OnSwapBuilding(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Started)
        {
            m_OnPlayerSwapBuilding?.Invoke();   
        }
    }

    #endregion

    #endregion

    #region Utility Functions

    public void SetInputContext(string mapName)
    {
        playerInput.SwitchCurrentActionMap(mapName);
    }

    #endregion

    #region Damage Interface
    public void Damage(GameObject source, float damageAmount)
    {
        m_OnPlayerDamage?.Invoke(damageAmount); 
    }
    #endregion
}

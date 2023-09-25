using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementBehaviour : MonoBehaviour
{
    #region User Paramters
    [Header("Movement")]
    [Tooltip("The speed the player will move when they have fully accelerated.")]
    [SerializeField]
    private float maxSpeed = 5.0f;

    [Tooltip("The rate the player accelerates towards max speed once movement input is received.")]
    [SerializeField]
    private float accelerationRate = 1.0f;

    [Tooltip("The rate the player decelerates once ther eis no movement input")]
    [SerializeField]
    private float decelerationRate = 1.0f;

    [Tooltip("Flat multiplier for all speed attributes. Use if you want to scale this behaviour uniformly.")]
    [SerializeField]
    private float speedMultiplier = 0.0f;

    [Tooltip("Rate the player will turn to face the direction of movement input.")]
    [SerializeField]
    private float turnSpeed = 0.1f;
    // Start is called before the first frame update
    #endregion

    #region Variables

    private Vector3 _movementInput;
    private Vector3 _targetMoveVector;
    private Vector3 _moveVector;
    private float movementSpeed;

    #endregion

    #region Components
    private Rigidbody _rigidbody;
    #endregion

    #region Runtime Callbacks
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        AcceleratePlayer();
        MovePlayer();
        RotatePlayer();
    }

    #endregion

    #region Methods
    public void GetMovementInput(Vector2 input)
    {
        _movementInput = new Vector3(input.x, 0.0f, input.y);
    }


    private void RotatePlayer()
    {
        //if moving, rotate player to face the direction they are moving
        if (_movementInput.sqrMagnitude > 0.01f)
        {
            Quaternion rotation = Quaternion.Slerp(_rigidbody.rotation,
                Quaternion.LookRotation(_movementInput), turnSpeed);
            _rigidbody.MoveRotation(rotation);
        }
    }

    private void MovePlayer()
    {
        _rigidbody.MovePosition(_rigidbody.position + _moveVector * Time.deltaTime);
    }

    private void AcceleratePlayer()
    {
        //if moving, accelerate to max speed
        if (_movementInput.sqrMagnitude > 0.01f)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, maxSpeed, accelerationRate*Time.deltaTime); //linearly ramp to max speed
            movementSpeed = Mathf.Clamp(movementSpeed, 0, maxSpeed); //safety

            _moveVector = _movementInput * movementSpeed;
            _targetMoveVector = _movementInput * maxSpeed;
            _moveVector = Vector3.Lerp(_moveVector, _targetMoveVector, accelerationRate*Time.deltaTime); //ramp up to max move vector

            return;
        }

        //if no movement input, decelerate back down to no movement
        _moveVector = Vector3.Lerp(_moveVector, new Vector3(0,0,0), decelerationRate * Time.deltaTime); //ramp down to 0 movement
    }

    #endregion

}

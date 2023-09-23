using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementBehaviour : MonoBehaviour
{
    #region User Paramters
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float targetMovementSpeed;
    [SerializeField]
    private float accelerationRate;
    [SerializeField]
    private float decelerationRate;
    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    private float turnSpeed = 0.00001f;
    // Start is called before the first frame update
    #endregion

    #region Variables

    private Vector3 _movementInput;

    #endregion

    #region Components
    private Rigidbody _rigidbody;
    #endregion

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void GetMovementInput(Vector2 input)
    {
        Debug.Log(input);
        _movementInput = new Vector3(input.x, 0.0f, input.y);
    }



    // Update is called once per frame
    void Update()
    {
        //accelerate/decelerate towards target speed
        //Vector3 targetPosition = transform.position+_movementInput;
        _rigidbody.MovePosition(_rigidbody.position+_movementInput * movementSpeed * Time.deltaTime);

        if(_movementInput.sqrMagnitude > 0.01f)
        {
            Quaternion rotation = Quaternion.Slerp(_rigidbody.rotation,
                Quaternion.LookRotation(_movementInput), turnSpeed);
            _rigidbody.MoveRotation(rotation);
        }
        //move based on current speed
    }
}

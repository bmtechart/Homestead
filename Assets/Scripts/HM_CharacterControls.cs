using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_CharacterControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //I have added a simple movement controls for our games. you can make other additions to make it more smoother to control
        float Xpos = Input.GetAxis("Horizontal");
        float Ypos = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(Xpos, 0, Ypos).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //now we need to make a simple shooting system to the controls so the player can shoot/hit things.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HM_CharacterControls : MonoBehaviour
{

    Rigidbody rb;

    public float moveSpeed = 5f;
    public float lookSpeed;

    public float dashSpeed = 10f;
    public float dashTime = 1f;
    public float dashCooldown = 2f;
    bool Dashing;
    bool canDash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Dashing)
        {
            return;
        }

        //I have added a simple movement controls for our games. you can make other additions to make it more smoother to control
        float Xpos = Input.GetAxis("Horizontal");
        float Ypos = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(Xpos, 0, Ypos).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //now we need to make a simple shooting system to the controls so the player can shoot/hit things.

        if (Input.GetKeyDown(KeyCode.Space) && canDash) 
        { 
          StartCoroutine(Dash());
        }

    }


    private IEnumerator Dash()
    {
        canDash = false;

        Dashing = true;

        rb.velocity = new Vector3(moveDirection.Xpos * dashSpeed, moveDirection.Ypos * dashSpeed);

        yield return new WaitForSeconds(dashTime);
        Dashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }


}

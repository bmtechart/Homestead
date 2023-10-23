using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMoving()
    {
        if (!Animator) { return; }
        Animator.SetTrigger("startMoving");
    }

    public void StopMoving()
    {
        if (!Animator) { return; }
        Animator.ResetTrigger("startMoving");
        Animator.SetTrigger("stopMoving");
    }

    public void StartAttack()
    {
        if(!Animator) { return; }
        Animator.SetTrigger("startAttack");
        Debug.Log("AttackStart");
    }

    public void StopAttack()
    {
        if (!Animator) { return; }
        Animator.SetTrigger("stopAttack");
    }

    public void OnDeath()
    {
        if(Animator)
        {
            Animator.SetTrigger("onDeath");
        }
    }
}

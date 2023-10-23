using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationController : AnimationController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameManager.GetInstance().m_OnGameOver.AddListener(OnGameOver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMoveStart()
    {
        if (!Animator) return;
        Animator.SetBool("isMoving", true);
    }

    public void OnMoveEnd()
    {
        if (!Animator) return;
        Animator.SetBool("isMoving", false);
    }

    public void OnAttackStart()
    {
        if (!Animator) return;
        //Animator.ResetTrigger("EndAttack");
        Animator.SetBool("isAttacking", true);
    }

    public void OnAttackEnd()
    {
        if (!Animator) return;
        Animator.SetBool("isAttacking", false);
    }

    public void OnDeath()
    {
        if (!Animator) return;
        Animator.SetTrigger("Death");
    }

    public void OnGameOver(bool playerVictory)
    {
        //if the player wins, do not trigger animation
        //if player loses, play cheering animation
        if (playerVictory) return;
        if (!Animator) return;
        Animator.SetTrigger("Victory");
    }
}

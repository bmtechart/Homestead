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
        Animator.SetBool("isMoving", true);
    }

    public void OnMoveEnd()
    {
        Animator.SetBool("isMoving", false);
    }

    public void OnAttackStart()
    {
        //Animator.ResetTrigger("EndAttack");
        Animator.SetBool("isAttacking", true);
    }

    public void OnAttackEnd()
    {
        Animator.SetBool("isAttacking", false);
    }

    public void OnDeath()
    {
        Animator.SetTrigger("Death");
    }

    public void OnGameOver(bool playerVictory)
    {
        //if the player wins, do not trigger animation
        //if player loses, play cheering animation
        if (playerVictory) return;
        Animator.SetTrigger("Victory");
    }
}

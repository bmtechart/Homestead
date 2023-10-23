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
        Animator.SetTrigger("StartMove");
    }

    public void OnMoveEnd()
    {
        Animator.SetTrigger("EndMove");
    }

    public void OnAttackStart()
    {
        Animator.SetTrigger("StartAttack");
    }

    public void OnAttackEnd()
    {
        Animator.SetTrigger("EndAttack");
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

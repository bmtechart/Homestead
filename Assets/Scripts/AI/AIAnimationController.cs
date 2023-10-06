using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationController : MonoBehaviour
{
    public GameObject skeletalMesh;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        if (skeletalMesh == null) { return; }
        _animator = skeletalMesh.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMoving()
    {
        if (!_animator) { return; }
        _animator.SetTrigger("WalkStart");
    }

    public void StopMoving()
    {
        if (!_animator) { return; }
        _animator.ResetTrigger("WalkStart");
        _animator.SetTrigger("WalkStop");
    }

    public void StartAttack()
    {
        if (!_animator) { return; }
        _animator.SetTrigger("AttackStart");
    }

    public void StopAttack()
    {
        if (!_animator) { return; }
        _animator.SetTrigger("AttackStop");
    }
}

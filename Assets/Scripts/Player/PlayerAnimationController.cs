using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
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
        _animator.SetTrigger("startMoving");
    }

    public void StopMoving()
    {
        if (!_animator) { return; }
        _animator.ResetTrigger("startMoving");
        _animator.SetTrigger("stopMoving");
    }

    public void StartAttack()
    {
        if(!_animator) { return; }
        _animator.SetTrigger("startAttack");
    }

    public void StopAttack()
    {
        if (!_animator) { return; }
        _animator.SetTrigger("stopAttack");
    }
}

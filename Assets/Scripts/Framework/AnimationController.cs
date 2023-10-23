using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] protected GameObject SkeletalMesh;
    protected Animator Animator;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (!SkeletalMesh)
        {
            Debug.Log("Animation Controller has no skeletal mesh game object assigned!");
            return;
        }

        Animator = SkeletalMesh.GetComponent<Animator>();
        if(!Animator)
        {
            Debug.Log("Skeletal mesh has no animator assigned.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

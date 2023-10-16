using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackBehaviour : MonoBehaviour
{
    //mesh game object
    [Tooltip("Weapon prefab to pass attack callbacks.")]
    [SerializeField] private GameObject weapon;

    [Tooltip("Amount of damage done when this attack hits.")]
    [SerializeField] private float damage = 10.0f;

    [Tooltip("Time in seconds which must elapse between attacks.")]
    [SerializeField] private float attackCooldown;

    //components
    private CapsuleCollider capsuleCollider;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //component requires weapon game object
        if(!weapon) { Debug.Log("No weapon equipped! Player unable to attack!"); }
        capsuleCollider = weapon.GetComponent<CapsuleCollider>();


    }

    public void StartAttack()
    {
        capsuleCollider.enabled=true;
    }

    public void EndAttack()
    {
        capsuleCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        AIController aiController = other.GetComponent<AIController>();
        if(!aiController) { return; } //we only want to damage enemies

        aiController.Damage(gameObject, damage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

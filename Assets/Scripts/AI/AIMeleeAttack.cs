using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMeleeAttack : AIAttackBehaviour
{
    [SerializeField] private Collider WeaponCollider;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (!WeaponCollider)
        {
            Debug.Log("No weapon Collider assigned to melee attack");
            return;
        }
       
        WeaponCollider.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void OnStartAttack()
    {
        base.OnStartAttack();
        WeaponCollider.enabled = true;
    }

    
}

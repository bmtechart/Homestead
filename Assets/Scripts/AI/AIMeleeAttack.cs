using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMeleeAttack : AIAttackBehaviour
{
    [SerializeField] private WeaponController Weapon;
    [SerializeField] private float Damage;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (!Weapon)
        {
            Debug.Log("No weapon Collider assigned to melee attack");
            return;
        }
       
        Weapon.Damage = Damage;
        

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void OnStartAttack()
    {
        base.OnStartAttack();
        Weapon.EnableWeapon();
    }

    public override void OnStopAttack()
    {
        base.OnStopAttack();
        Weapon.DisableWeapon();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    public GameObject owner;
    public GameObject target;
    public float Damage;
    private Collider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        if(!GetComponent<Collider>())
        {
            return;
        }
        DisableWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableWeapon()
    {
        _collider.enabled = true;
    }

    public void DisableWeapon()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != target) return;
        IDamageable damageInterface = other.GetComponent<IDamageable>();
        if (damageInterface == null) 
        {
            Debug.Log("struck actor does not have a damage interface implemented");
            return; 
        }

        damageInterface.Damage(owner, Damage);

    }
}

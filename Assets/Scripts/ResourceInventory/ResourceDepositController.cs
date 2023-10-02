using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDepositController : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update

    [SerializeField]
    private float health = 1.0f;

    public void Damage(float damageAmount)
    {
        Debug.Log("Damaged this resource node!");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

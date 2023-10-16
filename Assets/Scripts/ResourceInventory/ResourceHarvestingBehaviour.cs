using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHarvestingBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TraceForValidResourceNode()
    {
        RaycastHit hit;

        Vector3 p1 = transform.position;
        if (Physics.SphereCast(p1, 10, transform.forward, out hit, 10))
        {
            IDamageable damageInterface = hit.rigidbody.gameObject.GetComponent<IDamageable>();
            if (damageInterface != null) { return; }
            damageInterface.Damage(gameObject, 5.0f);

        }
        //draw a sphere trace to identify resource nodes in front of the player within their tool's reach. 
    }

    void DamageResourceNode() 
    {
        //damage a resource node
        //get damageable interface from resource node controller
        //call damage function from damageable interface
    }
}

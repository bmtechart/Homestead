using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject source;
    public GameObject target;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float collisionDistance;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!target) { return; }

        transform.position = (transform.position - target.transform.position) * (moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target.transform.position) <= collisionDistance)
        {
            //if target can be damaged, damage it
            IDamageable damageableTarget = target.GetComponent<IDamageable>();
            if(damageableTarget != null)
            {
                damageableTarget.Damage(source, Damage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

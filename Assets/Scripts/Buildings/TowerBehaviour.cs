using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TargetPriority
{
    Nearest,
    Farthest,
    Strongest,
    Weakest
}
[RequireComponent(typeof(SphereCollider))]
public class TowerBehaviour : MonoBehaviour
{
    //exposed parameters
    [SerializeField] private TargetPriority targetPriority;
    [SerializeField] private GameObject turret;
    [SerializeField] private AIController target;
    [SerializeField] private float attackDelay;
    [SerializeField] private GameObject projectile;

    //variables
    private List<AIController> enemiesInRange;
    

    //components
    private SphereCollider towerRangeTrigger;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<AIController>();
        towerRangeTrigger = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.isEditor)
        {
            if(towerRangeTrigger != null) 
            {
                towerRangeTrigger.isTrigger = true;
            }
        }

        if(!target) { GetNextAvailableEnemy(); }
    }
    
    private void GetNextAvailableEnemy()
    {
        if(enemiesInRange.Count == 0) { return; }

        target = enemiesInRange[0];

    }

    private void OnTriggerEnter(Collider other)
    {
        //if we don't see an enemy
        AIController enemy = other.GetComponent<AIController>();
        if(!enemy) { return; }
        if(enemiesInRange.Contains(enemy)) { return; } //if enemy is already in range

        enemiesInRange.Add(enemy);
        target = enemy;
    }

    private void OnTriggerExit(Collider other)
    {
        AIController enemy = other.GetComponent<AIController>();
        if(!enemy) { return; }
        if(!enemiesInRange.Contains(enemy)) { return; }

        enemiesInRange.Remove(enemy);
    }

    IEnumerator Attack()
    {
        //spawn projectile

        Instantiate(projectile, transform);


        yield return new WaitForSeconds(attackDelay);
    }
}

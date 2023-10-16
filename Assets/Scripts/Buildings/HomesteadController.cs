using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
public class HomesteadController : BuildingController
{
    private HealthBehaviour healthBehaviour;    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        healthBehaviour = GetComponent<HealthBehaviour>();
        if(healthBehaviour)
        {
            healthBehaviour.OnDeath.AddListener(GameManager.GetInstance().GameOver);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Damage(GameObject source, float damageAmount)
    {
        if(healthBehaviour)
        {
            healthBehaviour.Damage(damageAmount);
        }
    }
}

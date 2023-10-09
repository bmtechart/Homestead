using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public WaveManager waveManager;


    public float Speed = 3.0f;

    private float countdown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        waveManager = GetComponentInParent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {transform.Translate(transform.forward * Speed * Time.deltaTime);

        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Destroy(gameObject);

            waveManager.waves[waveManager.CurrentWave].EnemeyCount--; // add to ai when they are defeated
        }  
        
    }
}

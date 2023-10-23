using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : Singleton<WaveManager>
{
    [SerializeField]
    private float countdown;

    public Wave[] waves;

    public int CurrentWave = 0;


    public Transform[] Spawnpoints;
    private bool ReadyCountDown;

    private void Start()
    {
        ReadyCountDown = true;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].EnemeyCount = waves[i].enemies.Length;
        }
    }

    private void Update()
    {

        if (CurrentWave >= waves.Length)
        {
            //End of the game
            return;
        }


        if (ReadyCountDown == true)
        {
            countdown -= Time.deltaTime;
        }


        if (countdown <= 0)
        {
            ReadyCountDown = false;

            countdown = waves[CurrentWave].NextWaveTime;
            StartCoroutine(SpawnWave());
        }

        if (waves[CurrentWave].EnemeyCount == 0)
        {
            ReadyCountDown = true;

            CurrentWave++;
        }
    }

    private IEnumerator SpawnWave()
    {
        if (CurrentWave < waves.Length)
        {
            for (int i = 0; i < waves[CurrentWave].enemies.Length; i++)
            {
                Transform Spawn = Spawnpoints[Random.Range(0, Spawnpoints.Length)]; // get random spawn point

                GameObject enemy = Instantiate(waves[CurrentWave].enemies[i]); //this is broken, spawn game objects and get the controllers from the game objects
                enemy.transform.position = Spawn.transform.position;
                AIController enemyController = enemy.GetComponent<AIController>();
                if (enemyController) enemyController.Init();
                //enemy.transform.SetParent(Spawn.transform);

                yield return new WaitForSeconds(waves[CurrentWave].BetweenEnemiesTime);
            }
        }
    }



    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
        public float BetweenEnemiesTime;
        public float NextWaveTime;

        public int EnemeyCount;
    }
}

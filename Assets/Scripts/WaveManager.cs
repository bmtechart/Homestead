using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WaveManager : MonoBehaviour
{
    public UnityEvent StartWave;

    public UnityEvent WaveComplete;

    public UnityEvent EndGame;

    [SerializeField]
    private float countdown;

    public Wave[] waves;

    public int CurrentWave = 0;


    public Transform[] Spawnpoints;



    private bool ReadyCountDown;


    public GameObject EndMenu;


    private void Start()
    {
        EndMenu.SetActive(false);

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
            //End of final wave

            EndGame?.Invoke();
            
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
                Transform Spawn = Spawnpoints[Random.Range(0, Spawnpoints.Length)];

                AIController enemy = Instantiate(waves[CurrentWave].enemies[i], Spawn.transform);

                enemy.transform.SetParent(Spawn.transform);

                yield return new WaitForSeconds(waves[CurrentWave].BetweenEnemiesTime);
            }
        }
    }

    public void AllWavesFinished()
    {
        //End of the game

        EndMenu.SetActive(true);

        Time.timeScale = 0;  
        
        Cursor.lockState = CursorLockMode.None;
        
    }



    [System.Serializable]
    public class Wave
    {
        public AIController[] enemies;
        public float BetweenEnemiesTime;
        public float NextWaveTime;

        public int EnemeyCount;
    }
}

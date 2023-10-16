using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    /*
[SerializeField]
private GameObject SpawnPoint;

[SerializeField]
private float countdown;

public Wave[] waves;

public int CurrentWave = 0;


private bool ReadyCountDown; 

private void Start()
{
    ReadyCountDown = true;

    for (int i = 0; i < waves.Length; i++)
    {
        //waves[i].EnemeyCount = waves[i].enemies.Length;
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


    if (countdown <= 0 )
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
            //Enemy enemy = Instantiate(waves[CurrentWave].enemies[i], SpawnPoint.transform);

            enemy.transform.SetParent(SpawnPoint.transform);

            yield return new WaitForSeconds(waves[CurrentWave].BetweenEnemiesTime);
        }
    }
}



[System.Serializable]
public class Wave
{
    //public Enemy[] enemies;
    public float BetweenEnemiesTime;
    public float NextWaveTime;

    public int EnemeyCount;
}
 */
}

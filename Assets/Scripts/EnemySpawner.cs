using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    private WaveConfigSO currentWave;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        foreach (WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(currentWave.GetEnemyPrefab(i), 
                       currentWave.GetStartingWaypoint().position, 
                       Quaternion.identity, 
                       transform);
              yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }

    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}

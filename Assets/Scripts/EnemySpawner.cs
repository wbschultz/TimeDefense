using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public Transform enemyprefab;

    public Transform spawnPoint;
    public float delayBetweenEnemies;
    public float timeBetweenWaves = 10f;
    private float countdown = 2f;

    private bool isSpawning = false;
    private int waveNumber = 0;
    private List<float> timeBetweenSpawns = new List<float>();
    private void Start()
    {
        timeBetweenSpawns.Add(0f);
        timeBetweenSpawns.Add(1f);
        timeBetweenSpawns.Add(3f);
        timeBetweenSpawns.Add(7f);
    }
    void Update()
    {
        if (countdown <=0)
        {
            isSpawning = true;
            waveNumber++;
            StartCoroutine(SpawnWave(timeBetweenSpawns, waveNumber));
            countdown = timeBetweenWaves;
        }
        if (isSpawning == false)
        {
            countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(List<float> timeBetweenSpawns, int enemiesPerSpawn)
    {
        
        
        for (int i = 0; i < timeBetweenSpawns.Count; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns[i]);
            for (int j = 0; j < enemiesPerSpawn; j++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(delayBetweenEnemies);
            }

            
        }
        isSpawning = false;
        Debug.Log("New Wave");
    }

    void SpawnEnemy()
    {
        Instantiate(enemyprefab, spawnPoint.position, spawnPoint.rotation);

    }
}


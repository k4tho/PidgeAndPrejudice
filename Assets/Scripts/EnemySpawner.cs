using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject PedestrianPrefab;
    public GameObject AnimalPatrolPrefab;
    public GameObject MilitaryPrefab;

    public int waveLevel;
    private Coroutine waveInProgress;

    private float[] xPositions = new float[] { -23f, 23f };
    private float[] leftYPositions = new float[] { -3f, 8f, 17.8f };
    private float[] rightYPositions = new float[] { -3f, 5.7f, 18.14f };

    private int numPedestriansSpawned;
    private int numAnimalPatrolsSpawned;
    private int numMilitariesSpawned;



    void Start()
    {
        waveLevel = 0;
        ResetEnemies();

        SpawnNextWave();
    }

    public void SpawnNextWave()
    {
        Readouts.UpdateWave();
        waveLevel++;
        numPedestriansSpawned = 0;
        numAnimalPatrolsSpawned = 0;
        numMilitariesSpawned = 0;

        SpawnPedestrians();
        SpawnAnimalPatrols();
        SpawnMilitaries();

        waveInProgress = StartCoroutine(WaitForNextWave());
    }

    IEnumerator WaitForNextWave()
    {

        yield return new WaitForSeconds(GetTimeBetweenWaves());
        SpawnNextWave();
    }

    private void SpawnPedestrians()
    {
        while (numPedestriansSpawned < GetNumEnemiesToSpawn(GameParameters.startNumPedestrian))
        {
            SpawnEnemy(PedestrianPrefab);
            numPedestriansSpawned++;
        }
    }

    private void SpawnAnimalPatrols()
    {
        while (numAnimalPatrolsSpawned < GetNumEnemiesToSpawn(GameParameters.startNumAnimalPatrol))
        {
            SpawnEnemy(AnimalPatrolPrefab);
            numAnimalPatrolsSpawned++;
        }
    }

    private void SpawnMilitaries()
    {
        while (numMilitariesSpawned < GetNumEnemiesToSpawn(GameParameters.startNumMilitary))
        {
            SpawnEnemy(MilitaryPrefab);
            numMilitariesSpawned++;
        }
    }

    private void SpawnEnemy(GameObject enemyType)
    {
        Instantiate(enemyType, FindSpawnLocation(), Quaternion.identity);
    }

    private Vector3 FindSpawnLocation()
    {
        int randomXSpawnLocation = Random.Range(0, 2);
        int randomYSpawnLocation = Random.Range(0, 3);

        Vector3 spawnLocation;

        if (randomXSpawnLocation == 0)
        {
            spawnLocation = new Vector3(xPositions[randomXSpawnLocation], leftYPositions[randomYSpawnLocation], 0f);
        }
        else
        {
            spawnLocation = new Vector3(xPositions[randomXSpawnLocation], rightYPositions[randomYSpawnLocation], 0f);
        }

        return spawnLocation;
    }

    private int GetNumEnemiesToSpawn(int numEnemiesFirstWave)
    {
        int multiplier = (waveLevel - 1) / GameParameters.addMoreEnemiesEveryXRounds;
        int numEnemiesToSpawn = numEnemiesFirstWave + (multiplier * GameParameters.numToIncreaseAmountOfEnemyBy);

        return numEnemiesToSpawn;
    }

    private int GetTimeBetweenWaves()
    {
        if ((GameParameters.maxWaitTimeBetweenWaves - waveLevel) < GameParameters.minWaitTimeBetweenWaves)
            return GameParameters.minWaitTimeBetweenWaves;
        return GameParameters.maxWaitTimeBetweenWaves - waveLevel;
    }

    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void ResetEnemies()
    {
        DestroyAllEnemies();
        numPedestriansSpawned = 0;
        waveLevel = 0;
        SpawnNextWave();
    }
}

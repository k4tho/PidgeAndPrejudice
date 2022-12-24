using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject PedestrianPrefab;
    public GameObject AnimalPatrolPrefab;
    public GameObject MilitaryPrefab;
    public Text WaveText;

    public int waveLevel;
    private Coroutine waveInProgress;

    private float[] xPositions;
    private float[] leftYPositions;
    private float[] rightYPositions;

    private int numEnemiesAlive;

    private int numPedestriansSpawned;
    private int numAnimalPatrolsSpawned;
    private int numMilitariesSpawned;

    void Start()
    {
        xPositions = new float[] { GameObject.Find("Wall").transform.position.x, GameObject.Find("Wall (1)").transform.position.x };
        leftYPositions = new float[] { GameObject.Find("SmallPlatform (3)").transform.position.y, GameObject.Find("SmallPlatform (9)").transform.position.y, GameObject.Find("LargePlatform (1)").transform.position.y, GameObject.Find("Ground").transform.position.y };
        rightYPositions = new float[] { GameObject.Find("MediumPlatform (6)").transform.position.y, GameObject.Find("SmallPlatform (7)").transform.position.y, GameObject.Find("LargePlatform (6)").transform.position.y, GameObject.Find("Ground").transform.position.y };

        //ResetGame();
        SpawnNextWave();
    }

    void Update()
    {
        if (numEnemiesAlive == 0)
        {
            waveInProgress = null;
            SpawnNextWave();
        }
    }

    public void EnemyIsKilled()
    {
        numEnemiesAlive--;
    }

    public void SpawnNextWave()
    {
        waveLevel++;
        ResetEnemies();

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
            numEnemiesAlive++;
        }
    }

    private void SpawnAnimalPatrols()
    {
        while (numAnimalPatrolsSpawned < GetNumEnemiesToSpawn(GameParameters.startNumAnimalPatrol))
        {
            SpawnEnemy(AnimalPatrolPrefab);
            numAnimalPatrolsSpawned++;
            numEnemiesAlive++;
        }
    }
    
    private void SpawnMilitaries()
    {
        while (numMilitariesSpawned < GetNumEnemiesToSpawn(GameParameters.startNumMilitary))
        {
            SpawnEnemy(MilitaryPrefab);
            numMilitariesSpawned++;
            numEnemiesAlive++;
        }
    }

    private void SpawnEnemy(GameObject enemyType)
    {
        Instantiate(enemyType, FindSpawnLocation(), Quaternion.identity);
    }

    private Vector3 FindSpawnLocation()
    {
        int randomXSpawnLocation = Random.Range(0, 2);
        int randomYSpawnLocation;
        
        if (randomXSpawnLocation == 0)
            randomYSpawnLocation = Random.Range(0, leftYPositions.Length);
        else
            randomYSpawnLocation = Random.Range(0, rightYPositions.Length);
        
        Vector3 spawnLocation;
        
        if (randomXSpawnLocation == 0)
        {
            spawnLocation = new Vector3(xPositions[randomXSpawnLocation] + 1f, leftYPositions[randomYSpawnLocation] + 1f, 0f);
        }
        else
        {
            spawnLocation = new Vector3(xPositions[randomXSpawnLocation] - 1f, rightYPositions[randomYSpawnLocation] + 1f, 0f);
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
    
    private void ResetEnemies()
    {
        numPedestriansSpawned = 0;
        numAnimalPatrolsSpawned = 0;
        numMilitariesSpawned = 0;
    }
    
    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
    
    public void ResetGame()
    {
        DestroyAllEnemies();
        ResetEnemies();
        waveLevel = 0;
    }
}

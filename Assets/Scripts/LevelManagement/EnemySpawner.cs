using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform enemiesParent;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScale = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning;

    private int GetNumEnemiesInWave() {
        return Mathf.RoundToInt(baseEnemies * (Mathf.Pow(currentWave, difficultyScale)));
    }

    private void InitializeWave() {
        isSpawning = true;
        enemiesLeftToSpawn = GetNumEnemiesInWave();
    }

    private void SpawnEnemy() {
        GameObject enemyPrefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)];
        Instantiate(enemyPrefabToSpawn, spawnPoint.position, Quaternion.identity, enemiesParent);
        enemiesLeftToSpawn--;
        enemiesAlive++;
        timeSinceLastSpawn = 0;
    }


    private void Start() {
        timeSinceLastSpawn = 1f / enemiesPerSecond;
        InitializeWave();
    }

    private void Update() {
        if (!isSpawning) {
            return;
        }

        timeSinceLastSpawn += Time.deltaTime;

        if (enemiesLeftToSpawn > 0 && timeSinceLastSpawn >= (1f / enemiesPerSecond)) {
            SpawnEnemy();
        }
    }

}

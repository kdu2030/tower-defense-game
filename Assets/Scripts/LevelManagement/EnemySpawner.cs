using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

    public static UnityEvent enemyDeathEvent { get; set; } = new UnityEvent();

    private void HandleEnemyDeath() {
        enemiesAlive--;
    }

    private int GetNumEnemiesInWave() {
        return Mathf.RoundToInt(baseEnemies * (Mathf.Pow(currentWave, difficultyScale)));
    }

    private void InitializeWave() {
        isSpawning = true;
        enemiesLeftToSpawn = GetNumEnemiesInWave();
        timeSinceLastSpawn = 1f / enemiesPerSecond;
    }

    private void SpawnEnemy() {
        GameObject enemyPrefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)];
        Instantiate(enemyPrefabToSpawn, spawnPoint.position, Quaternion.identity, enemiesParent);
        enemiesLeftToSpawn--;
        enemiesAlive++;
        timeSinceLastSpawn = 0;
    }

    private IEnumerator SetupNextWave() {
        currentWave++;
        yield return new WaitForSeconds(timeBetweenWaves);
        InitializeWave();
    }

    public void Awake() {
        enemyDeathEvent.AddListener(HandleEnemyDeath);
    }

    private void Start() {
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
        else if (enemiesAlive <= 0 && enemiesLeftToSpawn <= 0) {
            isSpawning = false;
            StartCoroutine(SetupNextWave());
        }

    }

}

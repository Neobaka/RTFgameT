using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public int enemyCount;
        public float timeBetweenSpawns;
    }

    public Transform spawnPoint;        // Точка спавна врагов
    public Transform[] waypoints;       // Массив точек пути
    public Wave[] waves;               // Массив волн
    public float timeBetweenWaves = 15f; // Время между волнами



    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    [Header("UI")]
    public TextMeshProUGUI waveCountdownText;
    public TextMeshProUGUI currentWaveText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not set in WaveManager!");
            return;
        }

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoints are not set in WaveManager!");
            return;
        }

        if (waves == null || waves.Length == 0)
        {
            Debug.LogError("Waves are not configured in WaveManager!");
            return;
        }

        foreach (Wave wave in waves)
        {
            if (wave.enemyPrefab == null)
            {
                Debug.LogError("Enemy prefab is not set in one of the waves!");
                return;
            }
        }

        StartCoroutine(StartWaveCountdown());
    }

    IEnumerator StartWaveCountdown()
    {
        while (currentWaveIndex < waves.Length)
        {
            Debug.Log($"Wave {currentWaveIndex + 1} starting in {timeBetweenWaves} seconds");
            yield return new WaitForSeconds(timeBetweenWaves);

            yield return StartCoroutine(SpawnWave());
            currentWaveIndex++;
        }

        Debug.Log("All waves completed!");
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[currentWaveIndex];

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Enemy prefab or spawn point is null!");
            return;
        }

        GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Initialize(waypoints);
            Debug.Log($"Enemy spawned at {spawnPoint.position}");
        }
        else
        {
            Debug.LogError("Enemy component not found on prefab!");
        }
    }
}
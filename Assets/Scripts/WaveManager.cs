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
        public GameObject[] enemyPrefabs;
        public int[] enemyCounts;
        public GameObject bossPrefab;
        public int bossCount;
        public float timeBetweenSpawns;
    }

    public Transform spawnPoint;        // Точка спавна врагов
    public Transform[] wayPoints1;
    public Transform[] wayPoints2;
    public Transform[] wayPoints3;
    public Transform[] wayPoints4;
    public Transform[][] allWayPoints;
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
        this.allWayPoints = new Transform[][] { wayPoints1, wayPoints2, wayPoints3, wayPoints4 };

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not set in WaveManager!");
            return;
        }

        foreach (var wayPoints in allWayPoints)
        {
            if (wayPoints == null || wayPoints.Length == 0)
            {
                Debug.LogError("Waypoints are not set in WaveManager!");
                return;
            }
        }

        if (waves == null || waves.Length == 0)
        {
            Debug.LogError("Waves are not configured in WaveManager!");
            return;
        }

        foreach (Wave wave in waves)
        {
            foreach (GameObject enemyPrefab in wave.enemyPrefabs)
            {
                if (enemyPrefab == null)
                {
                    Debug.LogError("Enemy prefab is not set in one of the waves!");
                    return;
                }
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

        //for (int i = 0; i < wave.enemyCount; i++)
        //{
        //    SpawnEnemy(wave.enemyPrefab);
        //    yield return new WaitForSeconds(wave.timeBetweenSpawns);
        //}
        //GameObject randomEnemyPrefab = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];

        for (int i = 0; i < wave.enemyPrefabs.Length; i++)
        {
            // Количество врагов для текущего типа
            int count = wave.enemyCounts[i];

            for (int j = 0; j < count; j++)
            {
                System.Random random = new();
                GameObject randomEnemyPrefab = wave.enemyPrefabs[random.Next(wave.enemyPrefabs.Length)];
                // Спаун врага
                SpawnEnemy(randomEnemyPrefab);
                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }
        }
        //спаун босса
        if (wave.bossPrefab != null && wave.bossCount > 0)
        {
            for (int i = 0; i < wave.bossCount; i++)
            {
                SpawnEnemy(wave.bossPrefab);
                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Enemy prefab or spawn point is null!");
            return;
        }

        System.Random randomWay = new();
        Transform[] randomWayPoints = allWayPoints[randomWay.Next(allWayPoints.Length)];

        GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        if (randomWayPoints == null || randomWayPoints.Length == 0)
        {
            Debug.LogError("Random waypoints array is null or empty! In WaveManeger");
            return;
        }

        foreach (Transform t in randomWayPoints)
        {
            if (t == null)
            {
                Debug.LogError("Once random null");
                return;
            }
        }

        if (enemy != null)
        {
            if (randomWayPoints == null || randomWayPoints.Length == 0)
            {
                Debug.LogError("Random waypoints array is null or empty! In WaveManeger");
                return;
            }
            enemy.Initialize(randomWayPoints);
            Debug.Log(randomWayPoints);
            Debug.Log($"Enemy spawned at {spawnPoint.position}");
        }
        else
        {
            Debug.LogError("Enemy component not found on prefab!");
        }
    }
}
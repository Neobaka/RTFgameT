using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using static WaveManager;

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
        public float waveTime;
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
    public Slider waveProgressSlider;  // Для шкалы прогресса


    public int CurrentWaveIndex => currentWaveIndex;




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

    //IEnumerator StartWaveCountdown()
    //{

    //    while (currentWaveIndex < waves.Length)
    //    {
    //        Debug.Log($"Wave {currentWaveIndex + 1} starting in {timeBetweenWaves} seconds");
    //        Debug.Log($"Не вызывается SpawnWave for wave {currentWaveIndex + 1}");
    //        yield return new WaitForSeconds(timeBetweenWaves);

    //        currentWaveText.text = $"Волна {currentWaveIndex + 1}";

    //        yield return StartCoroutine(SpawnWave());
    //        currentWaveIndex++;
    //    }

    //    Debug.Log("All waves completed!");
    //}
    IEnumerator StartWaveCountdown()
    {
        while (currentWaveIndex < waves.Length)
        {
            Debug.Log($"Wave {currentWaveIndex + 1} starting in {timeBetweenWaves} seconds");
            if (waves[currentWaveIndex].enemyPrefabs.Length == 0)
            {
                Debug.LogError($"Wave {currentWaveIndex + 1} has no enemy prefabs!");
                yield break;
            }

            if (waveProgressSlider != null)
            {
                waveProgressSlider.value = 0;  // Сбрасываем значение прогресса перед стартом
            }

            yield return new WaitForSeconds(timeBetweenWaves);

            yield return StartCoroutine(SpawnWave());
            currentWaveText.text = $"Волна {currentWaveIndex + 1}";
            Debug.Log($"Волна {currentWaveIndex + 1}");
            currentWaveIndex++;
        }

        Debug.Log("All waves completed!");
    }


    //IEnumerator SpawnWave()
    //{
    //    Debug.Log("Спаун вызван");
    //    Wave wave = waves[currentWaveIndex];

    //    //for (int i = 0; i < wave.enemyCount; i++)
    //    //{
    //    //    SpawnEnemy(wave.enemyPrefab);
    //    //    yield return new WaitForSeconds(wave.timeBetweenSpawns);
    //    //}
    //    //GameObject randomEnemyPrefab = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];

    //    float waveStartTime = Time.time;

    //    while (Time.time - waveStartTime < wave.waveTime)
    //    {
    //        Debug.Log("Spawning enemies...");
    //        for (int i = 0; i < wave.enemyPrefabs.Length; i++)
    //        {
    //            // Количество врагов для текущего типа
    //            int count = wave.enemyCounts[i];

    //            for (int j = 0; j < count; j++)
    //            {
    //                if (Time.time - waveStartTime >= wave.waveTime)
    //                {
    //                    Debug.Log($"Время волны {Time.time}");
    //                    //спаун босса
    //                    if (wave.bossPrefab != null && wave.bossCount > 0)
    //                    {
    //                        for (int k = 0; k < wave.bossCount; k++)
    //                        {
    //                            SpawnEnemy(wave.bossPrefab);
    //                            yield return new WaitForSeconds(wave.timeBetweenSpawns);
    //                        }
    //                    }
    //                    break;
    //                }
    //                System.Random random = new();
    //                GameObject randomEnemyPrefab = wave.enemyPrefabs[random.Next(wave.enemyPrefabs.Length)];
    //                // Спаун врага
    //                SpawnEnemy(randomEnemyPrefab);
    //                Debug.Log($"Заспаунен {randomEnemyPrefab.name}");
    //                yield return new WaitForSeconds(wave.timeBetweenSpawns);
    //            }
    //        }
    //    }  
    //}
    IEnumerator SpawnWave()
    {
        Debug.Log($"Starting spawn for wave {currentWaveIndex + 1}");

        Wave wave = waves[currentWaveIndex];
        if (wave.enemyPrefabs.Length != wave.enemyCounts.Length)
        {
            Debug.LogError($"Wave {currentWaveIndex + 1} has mismatched enemyCounts and enemyPrefabs lengths!");
            yield break;
        }

        float waveStartTime = Time.time;

        while (Time.time - waveStartTime < wave.waveTime)
        {
            //if (waveProgressSlider != null)
            //{
            //    UpdateProgressBar(Time.time, ti)
            //}

            for (int i = 0; i < wave.enemyPrefabs.Length; i++)
            {
                int count = wave.enemyCounts[i];
                for (int j = 0; j < count; j++)
                {
                    if (Time.time - waveStartTime >= wave.waveTime)
                    {
                        Debug.Log($"Wave time exceeded for wave {currentWaveIndex + 1}");
                        break;
                    }

                    GameObject randomEnemyPrefab = wave.enemyPrefabs[i];
                    SpawnEnemy(randomEnemyPrefab);
                    Debug.Log($"Spawned enemy {randomEnemyPrefab.name}");

                    yield return new WaitForSeconds(wave.timeBetweenSpawns);
                }
            }
            break;
        }

        Debug.Log($"Wave {currentWaveIndex + 1} completed spawning");
    }
    //public void UpdateProgressBar(Time.time, Time.startTime, Wave wave.wavetime) 
    //{
    //    ProgressValue = (Time.time - waveStartTime) / wave.waveTime;
    //    waveProgressSlider.localScale = new Vector3(ProgressValue, 1f, 1f);
    //}
    IEnumerator SpawnEndlessWaves()
    {
        Debug.Log($"Starting spawn for wave {currentWaveIndex + 1}");
        while (true)
        {
            // Рандомизируем набор врагов для бесконечной волны
            Wave wave = GenerateRandomWave();

            // Начинаем спавнить врагов в бесконечной волне
            Debug.Log("Starting endless wave...");
            yield return StartCoroutine(SpawnWave());

            // Пауза между волнами
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    Wave GenerateRandomWave()
    {
        Wave randomWave = new Wave();

        int enemyCount = Random.Range(3, 5);  // Количество типов врагов
        randomWave.enemyPrefabs = new GameObject[enemyCount];
        randomWave.enemyCounts = new int[enemyCount];

        for (int i = 0; i < enemyCount; i++)
        {
            randomWave.enemyPrefabs[i] = waves[Random.Range(0, waves.Length)].enemyPrefabs[Random.Range(0, waves[0].enemyPrefabs.Length)];
            randomWave.enemyCounts[i] = Random.Range(5, 20);  // Количество врагов
        }

        // Рандомный босс
        randomWave.bossPrefab = waves[Random.Range(0, waves.Length)].bossPrefab;
        randomWave.bossCount = Random.Range(0, 3);  // Иногда появляется босс

        randomWave.timeBetweenSpawns = 2f;
        randomWave.waveTime = 30f + Random.Range(0, 30);  // Время волны с рандомизацией

        return randomWave;
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
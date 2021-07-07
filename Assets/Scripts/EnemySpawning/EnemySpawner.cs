using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private GameManager gameManager;
    public static List<Transform> enemies;
    public Room[] allRooms;
    public List<Room> activeRooms;
    public Player player;
    public GameObject enemyPrefab;
    private Timer timer;

    public float timeBetweenSpawning = 10f;
    public float enemiesToSpawn = 5f;
    public float minRangeToSpawn = 3.5f;
    public float minSpawnTime = 0.5f, maxSpawnTime = 2f;

    private float timeBetweenSpawns = 0f;
#pragma warning disable 414
    private bool currentlySpawning = false;
    private bool betweenWaveTime = false;
#pragma warning restore 414

    private void Awake()
    {
        gameManager = GameManager.Instance;

        activeRooms.Add(player.CurrentRoom);
        timer = gameObject.AddComponent<Timer>();
        allRooms = FindObjectsOfType<Room>();
    }

    private void Start()
    {
        timer.Run(timeBetweenSpawning);

        enemies = new List<Transform>();
    }

    void Update()
    {
        if (!currentlySpawning && gameManager.EnemyCount <= 0)
        {
            if (!betweenWaveTime)
            {
                timer.Run(timeBetweenSpawning);
                betweenWaveTime = true;
            }

            print("Between wavetime: " + (int)timer.CurrentTime);
        }

        if (timer.IsComplete)
        {
            timer.Reset();
            betweenWaveTime = false;
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        currentlySpawning = true;
        print("Spawning new wave of enemies.");

        if (enemiesToSpawn > 0)
        {
            for (int i = 0; i < enemiesToSpawn + gameManager.WaveNumber; i++)
            {
                int activeRoomAmount = activeRooms.Count;
                int randomValue = Random.Range(0, activeRoomAmount);
                Room randomRoom = activeRooms[randomValue];

                if (randomRoom != null)
                {
                    Vector2 spawnPos = randomRoom.CalculateSpawnPoint(player.transform, minRangeToSpawn);

                    SpawnEnemy(enemyPrefab, spawnPos);
                }

                timeBetweenSpawns = Random.Range(minSpawnTime, maxSpawnTime);

                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }

        currentlySpawning = false;
        gameManager.WaveNumber++;

        StopCoroutine(nameof(SpawnWave));
    }

    private void SpawnEnemy(GameObject enemyPrefab, Vector2 spawnPos)
    {
        Vector2 pos = new Vector2(spawnPos.x, spawnPos.y);
        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemies.Add(enemy.transform);
    }
}

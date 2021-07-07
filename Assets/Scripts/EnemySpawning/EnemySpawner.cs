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

    public int WaveNumber
    {
        get
        {
            return waveNumber;
        }
        set
        {
            waveNumber = value;
        }
    }

    public float timeBetweenSpawning = 10f;
    public float enemiesToSpawn = 5f;
    public float minRangeToSpawn = 3.5f;
    public float minSpawnTime = 0.5f, maxSpawnTime = 2f;

    private int waveNumber = 0;
    private float timeBetweenSpawns = 0f;
    private bool waveSpawned = false;
#pragma warning disable 414
    private bool currentlySpawning = false;
#pragma warning restore 414

    private void Awake()
    {
        activeRooms.Add(player.CurrentRoom);
        gameManager = GameManager.Instance;
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
        if (waveSpawned)
        {
            timer.Run(timeBetweenSpawning);
            waveSpawned = false;
        }

        if (timer.IsComplete && !waveSpawned)
        {
            timer.Reset();
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        currentlySpawning = true;

        print("Spawning new wave of enemies.");

        if (enemiesToSpawn > 0)
        {
            for (int i = 0; i < enemiesToSpawn + WaveNumber; i++)
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

        WaveNumber++;
        currentlySpawning = false;
        waveSpawned = true;

        StopCoroutine(nameof(SpawnWave));
    }

    private void SpawnEnemy(GameObject enemyPrefab, Vector2 spawnPos)
    {
        Vector2 pos = new Vector2(spawnPos.x, spawnPos.y);
        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemies.Add(enemy.transform);
    }
}

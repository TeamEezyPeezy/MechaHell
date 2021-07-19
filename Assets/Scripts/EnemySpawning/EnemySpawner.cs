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

    public GameObject enemy_Basic;
    public GameObject enemy_Flying;

    private Timer timer;

    // Enemy Amounts
    public float enemiesAtStart = 5f;

    private float basicEnemiesToSpawn;
    private float flyingEnemiesToSpawn;

    private float baseDifficultyMultiplier = .75f;
    public float roomExtraDifficulty = 0.25f;
    private float difficultyMultiplier = 1f;

    public float RoomExtraDifficulty
    {
        get
        {
            float temp = roomExtraDifficulty * gameManager.RoomsOpen;
            print(temp);
            return temp;
        }
        set
        {
            roomExtraDifficulty = value;
        }
    }

    // Spawn times
    public float timeBetweenSpawning = 10f;
    public float minRangeToSpawn = 3.5f;
    public float minSpawnTime = 0.5f, maxSpawnTime = 2f;

    // Private * ------------- *
    private float timeBetweenSpawns = 0f;
#pragma warning disable 414
    private bool currentlySpawning = false;
    private bool betweenWaveTime = false;
#pragma warning restore 414

    private void CalculateEnemyAmounts()
    {
        difficultyMultiplier = baseDifficultyMultiplier + RoomExtraDifficulty;
        print("Difficulty multiplier = " + difficultyMultiplier);
        basicEnemiesToSpawn = basicEnemiesToSpawn + (gameManager.WaveNumber * difficultyMultiplier);
        print("Next wave basic enemies: " + basicEnemiesToSpawn);

        if (gameManager.WaveNumber >= 5 || gameManager.RoomsOpen >= 2)
        {
            flyingEnemiesToSpawn = flyingEnemiesToSpawn + (gameManager.WaveNumber * difficultyMultiplier);
            print("Next wave flying enemies: " + flyingEnemiesToSpawn);
        }
    }

    private void Awake()
    {
        gameManager = GameManager.Instance;
        enemies = new List<Transform>();
        timer = gameObject.AddComponent<Timer>();
    }

    private void Start()
    {
        gameManager.ResetGame();

        allRooms = FindObjectsOfType<Room>();
        if (player.currentRoom != null)
        {
            activeRooms.Add(player.CurrentRoom);
        }
        else
        {
            activeRooms.Add(player.startRoom);
        }

        basicEnemiesToSpawn = enemiesAtStart;
        timer.Run(timeBetweenSpawning);
    }

    void Update()
    {
        print("CurrentlySpawning: " + currentlySpawning + " EnemyCount: " + gameManager.EnemyCount);

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

        CalculateEnemyAmounts();

        if (basicEnemiesToSpawn > 0)
        {
            for (int i = 0; i < basicEnemiesToSpawn; i++)
            {
                int activeRoomAmount = activeRooms.Count;
                int randomValue = Random.Range(0, activeRoomAmount);
                Room randomRoom = activeRooms[randomValue];

                if (randomRoom != null)
                {
                    Vector2 spawnPos = randomRoom.CalculateSpawnPoint(player.transform, minRangeToSpawn);

                    SpawnEnemy(enemy_Basic, spawnPos);
                }
                else
                {
                    print("Random room is null, can't Spawn enemy!");
                }

                timeBetweenSpawns = Random.Range(minSpawnTime, maxSpawnTime);

                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }

        if (flyingEnemiesToSpawn > 0)
        {
            for (int i = 0; i < flyingEnemiesToSpawn; i++)
            {
                int activeRoomAmount = activeRooms.Count;
                int randomValue = Random.Range(0, activeRoomAmount);
                Room randomRoom = activeRooms[randomValue];

                if (randomRoom != null)
                {
                    Vector2 spawnPos = randomRoom.CalculateSpawnPoint(player.transform, minRangeToSpawn);

                    SpawnEnemy(enemy_Flying, spawnPos);
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

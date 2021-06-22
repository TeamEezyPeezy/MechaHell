using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private GameManager gameManager;

    private Room currentRoom;

    public Player player;

    public GameObject enemyPrefab;

    public float timeBetweenSpawning = 10f;

    public float timeBetweenSpawns = 2f;

    private bool waveSpawned = false;

    public float enemiesToSpawn = 10f;
    public float minSpawnTime= 0.5f, maxSpawnTime = 2f;

    public float minRangeToSpawn = 3f;

    private Timer timer;

    private bool currentlySpawning = false;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        timer = gameObject.AddComponent<Timer>();
    }

    private void Start()
    {
        timer.Run(timeBetweenSpawning);
        currentRoom = player.CurrentRoom;
    }

    void Update()
    {
        print(timer.CurrentTime);

        if (timer.IsComplete)
        {
            timer.Reset();

            if (!waveSpawned)
            {
                StartCoroutine(SpawnWave());
            }
        }
    }
    IEnumerator SpawnWave()
    {
        currentlySpawning = true;

        if (enemiesToSpawn > 0)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                if (currentRoom != null)
                {
                    Vector2 spawnPos = currentRoom.CalculateSpawnPoint(player.transform, minRangeToSpawn);

                    SpawnEnemy(enemyPrefab, spawnPos);
                }

                timeBetweenSpawns = Random.Range(minSpawnTime, maxSpawnTime);

                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }

        currentlySpawning = false;

        StopCoroutine(nameof(SpawnWave));
    }

    private void SpawnEnemy(GameObject enemyPrefab, Vector2 spawnPos)
    {
        Vector2 pos = new Vector2(spawnPos.x, spawnPos.y);

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}

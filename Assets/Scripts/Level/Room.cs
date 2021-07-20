using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    public bool IsRoomUnlocked { get; set; } = false;

    public int spawnPointAmount = 5;
    // Doors from this room to others.
    public Door[] myDoors;

    public GameObject spawnPointParent;

    public Door[] MyDoors => myDoors;

    public SpawnPoint[] spawnPoints;

    private Collider2D myCollider;

    private bool hasActivatedSpawnPoints = false;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!IsRoomUnlocked) return;

        if (!hasActivatedSpawnPoints)
        {
            SetSpawnPointsActive();
            hasActivatedSpawnPoints = true;
        }
    }

    public void SetSpawnPointsActive()
    {
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint != null) 
            {
                spawnPoint.Activate();
            }
        }
    }

    private void Start()
    {
        FillSpawnPoints();
    }

    void FillSpawnPoints()
    {
        spawnPoints = spawnPointParent.GetComponentsInChildren<SpawnPoint>();
    }

    public Vector2 CalculateSpawnPoint(Transform playerTransform, float minRange)
    {
        bool isValid = false;

        Vector2 playerPos = new Vector2(playerTransform.position.x, playerTransform.position.y);

        Vector3 t = new Vector3();

        while (!isValid)
        {
            int randomValue = Random.Range(0, spawnPoints.Length);

            t = spawnPoints[randomValue].transform.position;

            if (Vector2.Distance(t, playerPos) >= minRange) isValid = true;
        }

        return t;
    }
}

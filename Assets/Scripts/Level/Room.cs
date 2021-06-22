using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int spawnPointAmount = 5;
    // Doors from this room to others.
    public Door[] myDoors;

    public Door[] MyDoors
    {
        get
        {
            return myDoors;
        }
    }

    public Transform[] spawnPoints;

    private Collider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    public Vector2 CalculateSpawnPoint(Transform playerTransform, float minRange)
    {
        bool isValid = false;

        Vector2 playerPos = new Vector2(playerTransform.position.x, playerTransform.position.y);

        Vector3 t = new Vector3();

        while (!isValid)
        {
            Bounds bounds = myCollider.bounds;
            float offsetX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
            float offsetY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

            t = new Vector3(offsetX, offsetY);

            if (Vector2.Distance(t, playerPos) >= minRange) isValid = true;
        }

        return t;
    }
}

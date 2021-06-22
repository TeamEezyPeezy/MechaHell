using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float doorOpenRange = 5f;
    public Room currentRoom;

    public Room CurrentRoom
    {
        get
        {
            return currentRoom;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Room otherRoom = other.GetComponent<Room>();
        if (otherRoom == currentRoom) return;

        currentRoom = other.GetComponent<Room>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        Door closestDoor = GetClosestDoor(currentRoom.MyDoors);
        if (closestDoor != null)
        {
            if (Vector2.Distance(transform.position, closestDoor.transform.position) < doorOpenRange)
            {
                // Open door
                print("Open door.");

                closestDoor.OpenDoor();
            }
            else
            {
                print("Can't reach door.");
            }
        }
    }

    Door GetClosestDoor(Door[] doors)
    {
        Door tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Door t in doors)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}

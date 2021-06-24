using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float doorOpenRange = 5f;
    public Room currentRoom;
    public GameObject doorInfoText;
    private Timer timer;
    private float doorCheckInterval = 0.5f;

    private void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
    }

    private void Start()
    {
        timer.Run(doorCheckInterval);
    }

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

        if (timer.IsComplete)
        {
            CheckIfCloseToDoor();
            timer.Reset();
            timer.Run(doorCheckInterval);
        }
    }

    private void CheckIfCloseToDoor()
    {
        if (currentRoom != null)
        {
            Door closestDoor = GetClosestDoor(currentRoom.MyDoors);
            if (closestDoor != null)
            {
                if (Vector2.Distance(transform.position, closestDoor.transform.position) <= doorOpenRange && !closestDoor.isOpen && !closestDoor.doorPair.isOpen)
                {
                    // Show door info text.
                    doorInfoText.SetActive(true);
                }
                else
                {
                    doorInfoText.SetActive(false);
                }
            }
        }
    }

    private void OpenDoor()
    {
        if (currentRoom != null)
        {
            if (currentRoom.myDoors.Length <= 0) return;

            Door closestDoor = GetClosestDoor(currentRoom.MyDoors);
            if (closestDoor != null)
            {
                if (Vector2.Distance(transform.position, closestDoor.transform.position) <= doorOpenRange)
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

    void OnDrawGizmosSelected()
    {
        // Draw range of players reach to open doors.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, doorOpenRange);
    }
}

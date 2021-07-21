using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    public EnemySpawner enemySpawner;
    public Room currentRoom;
    public Room startRoom;
    private Timer timer;
    private Timer timer2;
    public GameObject keycardEffect;

    public int healthPoints = 100;
    public float doorOpenRange = 5f;
    private float doorCheckInterval = .25f;

    [SerializeField]
    private Animator playerUITakeDamageAnimation;
    [SerializeField]
    private AudioSource playerDamageTakenSound;
    [SerializeField]
    private ParticleSystem playerDamageTakenParticle;

    public Room CurrentRoom
    {
        get
        {
            return currentRoom;
        }
        set
        {
            currentRoom = value;
        }
    }

    private void Awake()
    {
        gameManager = GameManager.Instance;
        timer = gameObject.AddComponent<Timer>();
        timer2 = gameObject.AddComponent<Timer>();
        startRoom = currentRoom;
    }

    private void Start()
    {
        timer.Run(doorCheckInterval);
        timer2.Run(2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }

        if (timer2.IsComplete)
        {
            RandomCheck();
            timer2.Reset();
            timer2.Run(2f);
        }

        if (timer.IsComplete)
        {
            CheckIfCloseToDoor();
            timer.Reset();
            timer.Run(doorCheckInterval);
        }
    }

    private void RandomCheck()
    {
        if (currentRoom != null)
        {
            Door closestDoor = GetClosestDoor(currentRoom.MyDoors);
            if (closestDoor != null)
            {
                if (Vector2.Distance(transform.position, closestDoor.transform.position) >= doorOpenRange)
                {
                    gameManager.uiController.ShowPlayerDoorInfo(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Keycard"))
        {
            gameManager.Keycards++;

            if (other.gameObject.tag.Equals("Keycard"))
            {
                if (keycardEffect != null)
                {
                    GameObject de = Instantiate(keycardEffect, transform.position, Quaternion.identity);
                    Destroy(de.gameObject, 3f);
                }

                Destroy(other.gameObject);
            }
        } 
        if (other.gameObject.tag.Equals("Room"))
        {
            Room otherRoom = other.GetComponent<Room>();
            if (otherRoom == currentRoom) return;

            currentRoom = otherRoom;

            if (currentRoom != null)
            {
                if (!currentRoom.IsRoomUnlocked)
                {
                    print("Room unlocked!");
                    otherRoom.IsRoomUnlocked = true;

                    // Add all active rooms to list so spawner can spawn to those spots always.
                    enemySpawner.activeRooms.Add(otherRoom);
                }
            }
        }
    }

    private void CheckIfCloseToDoor()
    {
        if (currentRoom != null)
        {
            Door closestDoor = GetClosestDoor(currentRoom.MyDoors);
            if (closestDoor != null)
            {
                if (Vector2.Distance(transform.position, closestDoor.transform.position) <= doorOpenRange && !closestDoor.isOpen && !closestDoor.doorPair.isOpen && gameManager.Keycards >= 1)
                {
                    gameManager.uiController.ShowPlayerDoorInfo(true);
                }
                else
                {
                    gameManager.uiController.ShowPlayerDoorInfo(false);
                }
            }
        }
    }

    private void OpenDoor()
    {
        if (currentRoom != null)
        {
            if (currentRoom.myDoors.Length <= 0) return;

            Door closestDoor = GetClosestDoor(CurrentRoom.MyDoors);
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

    public void TakeDamage(int amount)
    {
        healthPoints -= amount;

        if (playerUITakeDamageAnimation != null)
        {
            playerUITakeDamageAnimation.Play("playerUIDamageEffectAnimation");
        }

        if (playerDamageTakenParticle != null)
        {
            playerDamageTakenParticle.Play();
        }

        if (playerDamageTakenSound != null)
        {
            playerDamageTakenSound.Play();
        }

        if(healthPoints <= 0)
        {
            healthPoints = 0; // if hp went under 0, corrects visual information
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        gameManager.GameOver();
    }

    public void RefillHealth()
    {
        healthPoints = 100;
    }

    public void BuyBonushealth()
    {
        // cannot go over 150 hp
        if(healthPoints > 125)
        {
            healthPoints = 150;
        } else {
            healthPoints += 25;
        }
    }
}

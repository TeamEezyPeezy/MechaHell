using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;

    [SerializeField]
    private int money = 100;
    [SerializeField] 
    private int keyCards = 0;
    [SerializeField]
    private int waveNumber = 0;

    private int enemyCount = 0;
    private int roomsOpen = 1;
    private bool canDropKeycard = false;

    [HideInInspector]
    public int lastKeyDropWave = 0;

    private void Start()
    {
        FindPlayer();
    }

    protected override void Awake()
    {
        base.Awake();
        FindPlayer();
    }

    private void FindPlayer()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    public bool CanDropKeycard
    {
        get
        {
            if (WaveNumber % 5 == 0 && lastKeyDropWave != WaveNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public int RoomsOpen
    {
        get
        {
            return roomsOpen;
        }
        set
        {
            roomsOpen = value;
        }
    }

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

    public int EnemyCount
    {
        get
        {
            if (enemyCount <= 0)
            {
                return 0;
            }
            else
            {
                return enemyCount;
            }
        }
        private set
        {
            enemyCount = value;
        }
    }

    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
        }
    }

    public int Keycards
    {
        get
        {
            return keyCards;
        }
        set
        {
            keyCards = value;
        }
    }

    public void EnemyDied()
    {
        EnemyCount--;
        Money += 50;
    }

    public void EnemySpawned()
    {
        EnemyCount++;
    }
}

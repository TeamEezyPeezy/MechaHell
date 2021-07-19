using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public UiController uiController;

    public int startMoney, startKeyCards, startWave;

    private int money;
    private int keyCards;
    private int waveNumber;
    private int roomsOpen;
    private int enemyCount;

#pragma warning disable 414
    private bool canDropKeycard = false;
#pragma warning restore 414

    [HideInInspector]
    public int lastKeyDropWave;

    private void Start()
    {
        FindReferences();
    }

    public void ResetGame()
    {
        FindReferences();

        money = startMoney;
        keyCards = startKeyCards;
        waveNumber = startWave;
        roomsOpen = 1;
        lastKeyDropWave = 0;
        enemyCount = 0;
        player.healthPoints = 100;
    }

    protected override void Awake()
    {
        base.Awake();
        ResetGame();
    }

    private void FindReferences()
    {
        player = FindObjectOfType<Player>();
        uiController = FindObjectOfType<UiController>();
    }

    public bool CanDropKeycard
    {
        get
        {
            if (WaveNumber % 1 == 0 && lastKeyDropWave != WaveNumber)
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
            player.RefillHealth();
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
    public void GameOver()
    {
        uiController.OpenGameOverHud();
    }
}

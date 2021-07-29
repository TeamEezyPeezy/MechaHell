using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public enum GameState
    {
        Menu,
        InGame
    }

    public GameState CurrentGameState;

    public Player player;
    public UiController uiController;
    public PlayfabManager playfabManager;
    public EnemySpawner enemySpawner;
    public MainMenu mainMenu;

    public int startMoney, startKeyCards, startWave;

    public delegate void PlayerValueUpdate();
    public static event PlayerValueUpdate onMoneyChange;
    public static event PlayerValueUpdate onKeycardChange;
    public static event PlayerValueUpdate onWaveNumberChange;
    public static event PlayerValueUpdate onDoorOpen;
    public static event PlayerValueUpdate onReachWave10;


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

    private void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        ResetGame();
    }

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
        if(player != null) player.healthPoints = 100;
        if (playfabManager == null) playfabManager = GetComponent<PlayfabManager>();
    }

    private void FindReferences()
    {
        player = FindObjectOfType<Player>();
        uiController = FindObjectOfType<UiController>();
        enemySpawner = FindObjectOfType<EnemySpawner>();

        mainMenu = FindObjectOfType<MainMenu>();
        
    }

    public bool CanDropKeycard
    {
        get
        {
            if (WaveNumber % 2 == 0 && lastKeyDropWave != WaveNumber)
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

    public int WaveNumber
    {
        get
        {
            return waveNumber;
        }
        set
        {
            if (waveNumber == value) return;
            if (onWaveNumberChange != null) onWaveNumberChange();

            waveNumber = value;

            if (waveNumber == 10)
            {
                if (onDoorOpen != null) onDoorOpen();
                if (onReachWave10 != null) onReachWave10();
            }
            player.RefillHealth();
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
            if (money == value) return;
            if (onMoneyChange != null) onMoneyChange();

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
            if (onKeycardChange != null) onKeycardChange();
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
        if (playfabManager.hasName == false)
        {
            uiController.nameWindow.SetActive(true);
        } 
        else
        {
            playfabManager.SendLeaderboard(WaveNumber);
            uiController.leaderboardWindow.SetActive(true);
        }

        uiController.OpenGameOverHud();
    }
}

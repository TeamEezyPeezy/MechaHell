using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;

    [SerializeField]
    private int money = 100;
    [SerializeField] 
    private int keyCards = 0;

    private int enemyCount = 0;
    private int waveNumber = 4;

    private bool canDropKeycard = false;

    [HideInInspector]
    public int lastKeyDropWave = 0;

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

    protected override void Awake()
    {
        // Base awake creates singleton out of this class,
        // it stays in hierarchy even if scene is changed.
        base.Awake();
    }

    private void Update()
    {

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

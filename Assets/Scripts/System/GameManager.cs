using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;

    [SerializeField]
    private int money = 100;

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

    protected override void Awake()
    {
        // Base awake creates singleton out of this class,
        // it stays in hierarchy even if scene is changed.
        base.Awake();
    }

    private void Update()
    {

    }
}

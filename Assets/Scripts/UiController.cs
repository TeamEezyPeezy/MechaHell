using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public GameObject gamePausedUI, gameActiveUI;

    // Highscore
    public GameObject rowPrefab;
    public Transform rowsParent;
    public GameObject nameWindow;
    public GameObject leaderboardWindow;
    public TMP_InputField nameInput;
    // - End - 

    [SerializeField] private GameObject pauseHud;
    [SerializeField] private GameObject gameOverHud;

    [SerializeField] private GameObject doorInfoText;

    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI keyCardText;
    [SerializeField] private TextMeshProUGUI waveNumberText;

    private GameManager gameManager;
    private int previousHealhPoints = 0;

    [SerializeField] private TextMeshProUGUI moveSpeedLevelText;
    [SerializeField] private TextMeshProUGUI dashLevelTextText;
    [SerializeField] private TextMeshProUGUI rifleLevelText;
    [SerializeField] private TextMeshProUGUI sniperLevelText;
    [SerializeField] private TextMeshProUGUI bazookaLevelText;

    public PlayerMovement playerMovementReference;
    public ExplosiveGun bazookaReference;
    public GunSystem gunReference;
    public CooldownIcon coolDownReference;

    public MachineGunUpgradeCenter machineGunUpgradeReference;
    public PlayerStatsUpgradeCenter moveSpeedUpgradeReference;
    public SniperUpgradeCenter sniperUpgradeReference;
    public DashUpgradeStation dashUpgradeReference;
    public BazookaUpgradeCenter bazookaUpgradeReference;

    private void OnEnable()
    {
        gameManager = GameManager.instance;

        GameManager.onKeycardChange += this.UpdateKeyCardValue;
        GameManager.onMoneyChange += this.UpdateMoneyValue;
        GameManager.onWaveNumberChange += this.UpdateWaveNumber;
    }

    private void OnDisable()
    {
        GameManager.onKeycardChange -= this.UpdateKeyCardValue;
        GameManager.onMoneyChange -= this.UpdateMoneyValue;
        GameManager.onWaveNumberChange -= this.UpdateWaveNumber;
    }

    private void UpdateKeyCardValue()
    {
        print("Keycard value changed.");
        FlashText fa = keyCardText.GetComponent<FlashText>();
        if (fa != null)
        {
            fa.Flash(0.25f, 1);
        }
    }

    private void UpdateMoneyValue()
    {
        print("Money value changed.");
        FlashText fa = moneyText.GetComponent<FlashText>();
        if (fa != null)
        {
            fa.Flash(0.25f, 1);
        }
    }

    private void UpdateWaveNumber()
    {
        print("Wave value changed.");
        FlashText fa = waveNumberText.GetComponent<FlashText>();
        if (fa != null)
        {
            fa.Flash(.25f, 2);
        }
    }

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    void Start()
    {
        pauseHud.SetActive(false);
        gameOverHud.SetActive(false);
    }

    void Update()
    {
        CheckUserInput();

        UpdatePlayerTexts();
    }

    public void ShowPlayerDoorInfo(bool showInfo)
    {
        FadeText fa = doorInfoText.GetComponent<FadeText>();

        if (showInfo && fa.FadeValue <= 0f)
        {
            if (fa != null)
            {
                fa.FadeIn(0.25f);

                doorInfoText.SetActive(true);
            }
        }
        else if (!showInfo && fa.FadeValue >= 1f) 
        {
            if (fa != null)
            {
                fa.FadeOut(0.25f);
            }
        }
    }

    private void UpdatePlayerTexts()
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + gameManager.Money;
        }

        if (keyCardText != null)
        {
            keyCardText.text = "" + gameManager.Keycards;
        }

        if (waveNumberText != null)
        {
            waveNumberText.text = "" + gameManager.WaveNumber;
        }

        if (player != null)
        {
            if (player.healthPoints != previousHealhPoints)
            {
                //Debug.Log(player.healthPoints);
                hpText.text = "" + player.healthPoints;
                previousHealhPoints = player.healthPoints;
            }
        }

        if (moveSpeedLevelText != null)
        {
            moveSpeedLevelText.text = "Lvl " + moveSpeedUpgradeReference.movespeedLvl;
        }

        if (dashLevelTextText != null)
        {
            dashLevelTextText.text = "Lvl " + dashUpgradeReference.dashLvl;
        }

        if (rifleLevelText != null)
        {
            rifleLevelText.text = "Lvl " + machineGunUpgradeReference.rifleLvl;
        }

        if (sniperLevelText != null)
        {
            sniperLevelText.text = "Lvl " + sniperUpgradeReference.sniperLvl;
        }

        if (bazookaLevelText != null)
        {
            bazookaLevelText.text = "Lvl " + bazookaUpgradeReference.bazookaLvl;
        }
    }

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        DisableGameHud();
        pauseHud.SetActive(true);

        playerMovementReference.enabled = false;
        gunReference.enabled = false;
        bazookaReference.enabled = false;
        coolDownReference.enabled = false;

        Time.timeScale = 0f;
    }

    public void OnClickResumeButton()
    {
        EnableGameHud();
        pauseHud.SetActive(false);

        playerMovementReference.enabled = true;
        gunReference.enabled = true;
        bazookaReference.enabled = true;
        coolDownReference.enabled = true;

        Time.timeScale = 1f;
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }

    public void OnClickSubmitName()
    {
        gameManager.uiController.nameWindow.SetActive(false);
        gameManager.playfabManager.SubmitNameButton();
    }

    void DisableGameHud()
    {
        gameActiveUI.SetActive(false);
        gamePausedUI.SetActive(true);
    }

    void ReloadLeaderBoard()
    {
        gameManager.playfabManager.GetLeaderboard();
        Time.timeScale = 0f;
    }

    void EnableGameHud()
    {
        gameActiveUI.SetActive(true);
        gamePausedUI.SetActive(false);
    }

    public void OpenGameOverHud()
    {
        DisableGameHud();

        gameManager.enemySpawner.DisableEnemies();

        Invoke(nameof(ReloadLeaderBoard), 1f);
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}

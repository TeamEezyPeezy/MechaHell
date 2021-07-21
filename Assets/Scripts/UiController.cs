using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject pauseHud;
    [SerializeField] private GameObject gameOverHud;

    [SerializeField] private GameObject doorInfoText;
    private bool doorInfoEnabled = false;

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
        gameManager = GameManager.Instance;

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
        gameManager = GameManager.Instance;
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
        if(showInfo && doorInfoEnabled) return;
        if (!showInfo && !doorInfoEnabled) return;

        FadeText fa = doorInfoText.GetComponent<FadeText>();

        if (showInfo)
        {
            if (fa != null)
            {
                fa.FadeIn(0.25f);

                doorInfoEnabled = true;
                doorInfoText.SetActive(true);
            }
        }
        else
        {
            if (fa != null)
            {
                fa.FadeOut(0.25f);

                doorInfoEnabled = false;
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

        if(player.healthPoints != previousHealhPoints)
        {
            Debug.Log(player.healthPoints);
            hpText.text = "" + player.healthPoints;
            previousHealhPoints = player.healthPoints;
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
        pauseHud.SetActive(true);
        playerMovementReference.enabled = false;
        gunReference.enabled = false;
        bazookaReference.enabled = false;
        coolDownReference.enabled = false;

        Time.timeScale = 0f;
    }

    public void OnClickResumeButton()
    {
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

    public void OpenGameOverHud()
    {
        gameOverHud.SetActive(true);

        Time.timeScale = 0f;
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}

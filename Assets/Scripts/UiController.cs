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

    public PlayerMovement playerMovementReference;
    public ExplosiveGun bazookaReference;
    public GunSystem gunReference;
    public CooldownIcon coolDownReference;

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
            fa.Flash(0.25f);
        }
    }

    private void UpdateMoneyValue()
    {
        print("Money value changed.");
        FlashText fa = moneyText.GetComponent<FlashText>();
        if (fa != null)
        {
            fa.Flash(0.25f);
        }
    }

    private void UpdateWaveNumber()
    {
        print("Wave value changed.");
        FlashText fa = waveNumberText.GetComponent<FlashText>();
        if (fa != null)
        {
            fa.Flash(0.25f);
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

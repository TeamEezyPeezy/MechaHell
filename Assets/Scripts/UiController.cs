using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject pauseHud;
    [SerializeField] private GameObject gameOverHud;
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

    private void UpdatePlayerTexts()
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + gameManager.Money;
        }

        if (keyCardText != null)
        {
            keyCardText.text = "Keycards " + gameManager.Keycards;
        }

        if (waveNumberText != null)
        {
            waveNumberText.text = "Wave: " + gameManager.WaveNumber;
        }

        if(player.healthPoints != previousHealhPoints)
        {
            Debug.Log(player.healthPoints);
            hpText.text = "HP: " + player.healthPoints;
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

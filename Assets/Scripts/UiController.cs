using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject pauseHud;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI keyCardText;

    private GameManager gameManager;

    public PlayerMovement playerMovementReference;
    public ExplosiveGun bazookaReference;
    public GunSystem gunReference;
    public CooldownIcon coolDownReference;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseHud.SetActive(false);
    }

    // Update is called once per frame
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
        Time.timeScale = 0f;
        pauseHud.SetActive(true);
        playerMovementReference.enabled = false;
        gunReference.enabled = false;
        bazookaReference.enabled = false;
        coolDownReference.enabled = false;
    }

    public void OnClickResumeButton()
    {
        pauseHud.SetActive(false);
        Time.timeScale = 1f;
        playerMovementReference.enabled = true;
        gunReference.enabled = true;
        bazookaReference.enabled = true;
        coolDownReference.enabled = true;
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}

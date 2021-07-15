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
        Time.timeScale = 0f;
        pauseHud.SetActive(true);

    }

    public void OnClickResumeButton()
    {
        pauseHud.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }

    public void OpenGameOverHud()
    {
        Time.timeScale = 0f;
        gameOverHud.SetActive(true);
 
    }

    public void OnClickRestartButton()
    {
        // TODO fix somehow
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

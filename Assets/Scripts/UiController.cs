using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{

    [SerializeField] private GameObject pauseHud;

    // Start is called before the first frame update
    void Start()
    {
        pauseHud.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlsPanel, highscorePanel, pauseGamePanel, renamePanel, blackFadeObject;

    public TMP_InputField nameInput;
    public GameObject rowPrefab;
    public Transform rowsParent;
    public Animator fadeAnimation;
    private Timer timer;
    private float highScoreUpdateFreq = 3f;
    private bool highScoreOpen = false;

    private GameManager gameManager;

    private void Awake()
    {
        blackFadeObject.SetActive(true);
        fadeAnimation.Play("blackFadeAnimation1");

        timer = gameObject.AddComponent<Timer>();

        if (gameManager == null)
        {
            gameManager = GameManager.instance;
        }
        gameManager.ResetGame();
        gameManager.CurrentGameState = GameManager.GameState.Menu;
    }


    private void Update()
    {
        if (timer.IsComplete && highScoreOpen)
        {
            timer.Stop();
            RefreshHighScore();
            timer.Run(highScoreUpdateFreq);
        }
    }

    public void LoadGame()
    {
        StartCoroutine(ChangeScene(1, 1.5f));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenHighScore()
    {
        highScoreOpen = true;
        timer.Run(highScoreUpdateFreq);

        pauseGamePanel.SetActive(false);
        highscorePanel.SetActive(true);
        Invoke(nameof(RefreshHighScore), 0.5f);
    }

    public void CloseHighScore()
    {
        highScoreOpen = false;
        timer.Stop();

        pauseGamePanel.SetActive(true);
        highscorePanel.SetActive(false);
    }

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);
    }

    public void OpenRenameField()
    {
        renamePanel.SetActive(true);
    }

    public void CloseRenameField()
    {
        renamePanel.SetActive(false);
    }

    public void RefreshHighScore()
    {
        if (gameManager != null)
        {
            gameManager.playfabManager.GetLeaderboard();
        }
    }

    public void SubmitPlayerRename()
    {
        if (gameManager != null)
        {
            gameManager.playfabManager.SubmitNameButtonMenu();
            
            Invoke(nameof(RefreshHighScore), 0.5f);

            renamePanel.SetActive(false);
        }
    }

    IEnumerator ChangeScene(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlsPanel, highscorePanel, pauseGamePanel, renamePanel;

    public TMP_InputField nameInput;
    public GameObject rowPrefab;
    public Transform rowsParent;

    private GameManager gameManager;

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.instance;
        }

        gameManager.CurrentGameState = GameManager.GameState.Menu;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenHighScore()
    {
        pauseGamePanel.SetActive(false);
        highscorePanel.SetActive(true);
        gameManager.playfabManager.GetLeaderboard();
    }

    public void CloseHighScore()
    {
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
            renamePanel.SetActive(false);
        }
    }
}

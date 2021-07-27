using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    public GameManager gameManager;

    public bool hasName = false;

    private void Start()
    {
        gameManager = GameManager.instance;
        Login();
    }

    public void Login()
    {
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        print("Successful login/account create");

        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null) {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (name == null)
        {
            hasName = false;
        }
        else
        {
            hasName = true;
        }
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = gameManager.uiController.nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);

        SendLeaderboard(gameManager.WaveNumber);
        gameManager.uiController.leaderboardWindow.SetActive(true);
        GetLeaderboard();
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        print("Updated display name!");
    }

    void OnError(PlayFabError error)
    {
        print("Error while logging in");
        print(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "WaveScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        print("Successful leaderboard score sent!");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "WaveScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    public void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in gameManager.uiController.rowsParent) 
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(gameManager.uiController.rowPrefab, gameManager.uiController.rowsParent);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1 ).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            print(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}

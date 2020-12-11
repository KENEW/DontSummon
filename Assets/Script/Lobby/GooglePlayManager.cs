using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayManager : MonoBehaviour
{
    bool bWait = false;
    public Text text;

    void Awake()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        text.text = "no Login";
        OnLogin();
    }
    public void OnLogin()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool bSuccess) =>
            {
                if (bSuccess)
                {
                    text.text = Social.localUser.userName;
                    Debug.Log(Social.localUser.userName);
                }
                else
                {
                    text.text = "Fail";
                }
            });
        }
    }
    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        text.text = "Logout";
    }
    public void OnShowLeaderBoard()
    {
		text.text = "ShowLeaderBoard";
		Social.ReportScore(1000, GPGSIds.leaderboard_score, (bool bSuccess) =>
		{
			if (bSuccess)
			{
				text.text = "ReportLeaderBoard Success";
			}
			else
			{
				text.text = "ReportLeaderBoard Fail";
			}
		}
		);
		Social.ShowLeaderboardUI();
	}
    public void OnShowAchievement()
    {
        text.text = "ShowAchievement";
        Social.ShowAchievementsUI();
    }
}

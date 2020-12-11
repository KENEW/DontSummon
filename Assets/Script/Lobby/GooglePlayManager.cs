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
    }
    public void OnLogin()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool bSuccess) =>
            {
                if (bSuccess)
                {
                    Debug.Log("Success : " + Social.localUser.userName);
                    text.text = Social.localUser.userName;
                }
                else
                {
                    Debug.Log("Fall");
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
}

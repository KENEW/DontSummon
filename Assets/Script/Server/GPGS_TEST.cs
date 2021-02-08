using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGS_TEST : MonoBehaviour
{
    public Text text;

    void Awake()
    {
        GPGSInit();
    }
    private void GPGSInit()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
        .Builder()
        .RequestServerAuthCode(true)
        .RequestEmail()
        .RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;

        //GPGS START
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

                    Debug.Log("Email : " + PlayGamesPlatform.Instance.GetUserEmail());
                    Debug.Log("Token : " + PlayGamesPlatform.Instance.GetIdToken());
                    Debug.Log("AuthCode : " + PlayGamesPlatform.Instance.GetServerAuthCode());

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
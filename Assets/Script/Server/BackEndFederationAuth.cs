using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class BackEndFederationAuth : MonoSingleton<BackEndFederationAuth>
{
	public Text text;

	//private void Awake()
	//{
	//	GPGSInit();
	//}
	private void GPGSInit()
	{
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
		.Builder()
		.RequestServerAuthCode(false)
		.RequestEmail()
		.RequestIdToken()
		.Build();

		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
	}
	public void OnLogin()
	{
		GPGSInit();

		if (!Social.localUser.authenticated)
		{
			Social.localUser.Authenticate((bool bSuccess) =>
			{
				if (bSuccess)
				{
					Debug.Log("Success : " + Social.localUser.userName);
					GameManager.Instance.isGPSCheck = true;

					Debug.Log("Email : " + PlayGamesPlatform.Instance.GetUserEmail());
					Debug.Log("Token : " + PlayGamesPlatform.Instance.GetIdToken());
					Debug.Log("AuthCode : " + PlayGamesPlatform.Instance.GetServerAuthCode());

					Debug.Log("GoogleId - " + Social.localUser.id);
					Debug.Log("UserName - " + PlayGamesPlatform.Instance.GetUserDisplayName());

					OnClickGPSLogin();
				}
				else
				{
					LoginScreen.Instance.OpenLoginFailedPanel();
					Debug.Log("Fall");
				}
			});
		}
	}
	private string GPGSGetTokens()
	{
		if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
			string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
			return _IDtoken;
		}
		else
		{
			Debug.Log("접속되어있지 않습니다. 잠시 후 다시 시도해주세요.");
			LoginScreen.Instance.OpenLoginFailedPanel();

			return null;
		}
	}
	public void OnClickGPSLogin()
	{
		BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GPGSGetTokens(), FederationType.Google, "gpgs로 만든계정");

		if (BRO.IsSuccess())
		{
			Debug.Log("구글 토큰으로 뒤끝서버 로그인 성공 - 동기방식");

			switch(BRO.GetStatusCode())
			{
				case "200":
					Debug.Log("로그인 성공 / InDate값을 불러옵니다.");
					BackEndGameInfo.Instance.OnClickPublicContents();
					break;
				case "201":
					Debug.Log("회원가입 성공 / InDate값을 생성합니다.");
					BackEndGameInfo.Instance.OnClickInsertData();
					break;
			}

			LoginScreen.Instance.NextScreen();
		}
		else
		{
			if(!GameManager.Instance.isLoginCheck)
			{
				LoginScreen.Instance.OpenLoginFailedPanel();
			}

			switch (BRO.GetStatusCode())
			{
				case "200":
					Debug.Log("이미 회원가입된 회원");
					break;
				case "403":
					Debug.Log("차단된 사용자 입니다. 차단 사유 : " + BRO.GetErrorCode());
					break;
				default:
					Debug.Log("서버 공통 에러 발생" + BRO.GetMessage());
					break;
			}
		}
	}
	public void OnShowLeaderBoard()
	{
		if (!GameManager.Instance.isGPSCheck)
		{
			Debug.Log("구글 로그인이 되지 않았습니다.");
			OnClickGPSLogin();
			return;
		}

		Social.ShowLeaderboardUI();
	}
	public void OnSetLeaderBoard(int stage, int score)
	{
		if (!GameManager.Instance.isGPSCheck)
		{
			Debug.Log("구글 로그인이 되지 않았습니다.");
			return;
		}
		switch (stage)
		{
			case 1:
				Social.ReportScore(score, GPGSIds.leaderboard_chapter1, (bool bSuccess) =>
				{
					if (bSuccess)
					{
						Debug.Log("ReportLeaderBoard Success");
					}
					else
					{
						Debug.Log("ReportLeaderBoard Fall");
					}
				}
				);
				break;
			case 2:
				Social.ReportScore(score, GPGSIds.leaderboard_chapter2, (bool bSuccess) =>
				{
					if (bSuccess)
					{
						Debug.Log("ReportLeaderBoard Success");
					}
					else
					{
						Debug.Log("ReportLeaderBoard Fall");
					}
				}
				);
				break;
			case 3:
				Social.ReportScore(score, GPGSIds.leaderboard_chapter3, (bool bSuccess) =>
				{
					if (bSuccess)
					{
						Debug.Log("ReportLeaderBoard Success");
					}
					else
					{
						Debug.Log("ReportLeaderBoard Fall");
					}
				}
				);
				break;
		}
	}
	public void OnShowAchievement()
	{
		if (!GameManager.Instance.isGPSCheck)
		{
			Debug.Log("구글 로그인이 되지 않았습니다.");
			return;
		}

		Social.ShowAchievementsUI();
	}
	public void OnAddAchievement(string achieveID)
	{
		if (!GameManager.Instance.isGPSCheck)
		{
			Debug.Log("구글 로그인이 되지 않았습니다.");
			return;
		}
		switch (achieveID)
		{
			case "Start":
				Social.ReportProgress(GPGSIds.achievement, 100.0f, (bool bSuccess) =>
				{
					if (bSuccess)
					{
						Debug.Log("Start AddAchievement Success");
					}
					else
					{
						Debug.Log("Start AddAchievement Fall");
					}
				});
				break;
			case "Chapter1":
				Social.ReportProgress(GPGSIds.achievement_2, 100.0f, (bool bSuccess) =>
				{
					if (bSuccess)
					{
						Debug.Log("1 Stage AddAchievement Success");
					}
					else
					{
						Debug.Log("1 Stage AddAchievement Fall");
					}
				});
				break;
			case "Chapter2":
				Social.ReportProgress(GPGSIds.achievement_3, 100.0f, (bool bSuccess) =>
				{
					if (bSuccess)
					{
						Debug.Log("2 Stage AddAchievement Success");
					}
					else
					{
						Debug.Log("2 Stage AddAchievement Fall");
					}
				});
				break;
			case "Chapter3":
				Social.ReportProgress(GPGSIds.achievement_4, 100.0f, (bool bSuccess) =>
				{
					if (bSuccess)
					{
						Debug.Log("3 Stage AddAchievement Success");
					}
					else
					{
						Debug.Log("3 Stage AddAchievement Fall");
					}
				});
				break;
		}
	}
}

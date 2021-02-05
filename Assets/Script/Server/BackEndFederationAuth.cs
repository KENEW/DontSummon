﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class BackEndFederationAuth : SceneSingleTon<BackEndFederationAuth>
{
	public void OnClickGoogleServer()
	{
		OnLogin();
		BackEndGameInfo.Instance.OnClickPublicContents();
	}
	public void OnLogin()
	{
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
		.Builder()
		.RequestServerAuthCode(false)
		.RequestEmail()
		.RequestIdToken()
		.Build();

		//Cusomized information GPGS Init
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = false;

		//GPGS START
		PlayGamesPlatform.Activate();
		GoogleAuth();
		OnClickGPSLogin();
	}

	private void GoogleAuth()
	{
		if (PlayGamesPlatform.Instance.localUser.authenticated == false)
		{
			Social.localUser.Authenticate(success =>
			{
				if (success == false)
				{
					Debug.Log("구글 로그인 실패");
					return;
				}

				//Login Success
				GameManager.Instance.isGPSCheck = true;
				Debug.Log("GetIdToken - " + PlayGamesPlatform.Instance.GetIdToken());
				Debug.Log("Email - " + ((PlayGamesLocalUser)Social.localUser).Email);
				Debug.Log("GoogleId - " + Social.localUser.id);
				Debug.Log("UserName - " + Social.localUser.userName);
				Debug.Log("UserName - " + PlayGamesPlatform.Instance.GetUserDisplayName());
			});
		}
	}

	private string GetTokens()
	{
		if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
			string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
			return _IDtoken;
		}
		else
		{
			Debug.Log("접속되어있지 않습니다. 잠시 후 다시 시도해주세요.");
			GoogleAuth();
			return null;
		}
	}

	public void OnClickGPSLogin()
	{
		BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, "gpgs로 만든계정");

		if (BRO == null)
		{
			Debug.Log("BRO가 비워있습니다.");
			return;
		}
		if (BRO.IsSuccess())
		{
			Debug.Log("구글 토큰으로 뒤끝서버 로그인 성공 - 동기방식");
		}
		else
		{
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

	public void OnClickUpdateEmail()
	{
		BackendReturnObject BRO = Backend.BMember.UpdateFederationEmail(GetTokens(), FederationType.Google);
		if (BRO.IsSuccess())
		{
			Debug.Log("이메일 주소 저장 완료");
		}
		else
		{
			if (BRO.GetStatusCode() == "404")
			{
				Debug.Log("federationId not found, federationId을(를) 찾을 수 없습니다.");
			}
			else
			{
				Debug.Log("서버 공통 에러 발생" + BRO.GetMessage());
			}
		}
	}

	//이미 가입된 상태인지 확인
	public void OnClickCheckUserAuthenticate()
	{
		BackendReturnObject BRO = Backend.BMember.CheckUserInBackend(GetTokens(), FederationType.Google);
		if (BRO.GetStatusCode() == "200")
		{
			Debug.Log("가입 중인 계정입니다.");

			//해당 계정 정보
			Debug.Log(BRO.GetReturnValue());
		}
		else
		{
			Debug.Log("가입된 계정이 아닙니다.");
		}
	}

	//커스텀 계정을 패더레이션 계정으로 변경
	public void OnClickChangeCustomToFederation()
	{
		BackendReturnObject BRO = Backend.BMember.ChangeCustomToFederation(GetTokens(), FederationType.Google);

		if (BRO.IsSuccess())
		{
			Debug.Log("패더레이션 계정으로 변경 완료");
		}
		else
		{
			switch (BRO.GetStatusCode())
			{
				case "400":
					if (BRO.GetErrorCode() == "BadParameterException")
					{
						Debug.Log("이미 ChangeCustomToFederation 완료 되었는데 다시 시도한 경우");
					}
					else if (BRO.GetErrorCode() == "UndefinedParameterException")
					{
						Debug.Log("CustomLogin 하지 않은 상황에서 시도한 경우");
					}
					break;
				case "409":
					//이미 가입되어 있는 경우
					Debug.Log("Duplicated federationId, 중복된 federationId 입니다.");
					break;
			}
		}
	}

	public void OnShowLeaderBoard()
	{
		if (!GameManager.Instance.isGPSCheck)
		{
			Debug.Log("구글 로그인이 되지 않았습니다.");
			return;
		}

		Social.ShowLeaderboardUI();
	}
	public void OnSetLeaderBoard(int stage, int score)
	{
		if(!GameManager.Instance.isGPSCheck)
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
	//랭킹 대시보드 보기
	public void OnShowAchievement()
	{
		if (!GameManager.Instance.isGPSCheck)
		{
			Debug.Log("구글 로그인이 되지 않았습니다.");
			return;
		}

		Social.ShowAchievementsUI();
	}

	// 업적 추가
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

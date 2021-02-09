using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class BackEndAuthentication : SceneSingleTon<BackEndAuthentication>
{
	public InputField idInput;
	public InputField paInput;

	public void OneClickCustomServer()
	{
		OnClickLogin();
		//BackEndGameInfo.Instance.OnClickPublicContents();
	}
	public void OnClickSignUp()
	{
		BackendReturnObject BRO = Backend.BMember.CustomSignUp(idInput.text, paInput.text, "Test1");
		
		if(BRO.IsSuccess())
		{
			Debug.Log("회원가입 완료");
			BackEndGameInfo.Instance.OnClickInsertData();
		}
		else
		{
			string error = BRO.GetStatusCode();

			switch (error)
			{
				case "409":
					Debug.Log("중복된 customId가 존재하는 경우");
					break;
				default:
					Debug.Log("서버 공통 에러" + BRO.GetMessage());
					break;
			}
		}
	}

	public void OnClickLogin()
	{
		BackendReturnObject BRO = Backend.BMember.CustomLogin(idInput.text, paInput.text);

		if (BRO.IsSuccess())
		{
			BackEndGameInfo.Instance.OnClickPublicContents();
			Debug.Log("로그인 완료 / 정보 로드");
		}
		else
		{
			string error = BRO.GetStatusCode();

			switch (error)
			{
				case "401":
					Debug.Log("아이디 또는 비밀번호가 틀렸다.");
					break;
				case "403":
					Debug.Log("차단된 유저" + BRO.GetStatusCode());
					break;
				default:
					Debug.Log("서버 공통 에러 발생" + BRO.GetMessage());
					break;
			}
		}
	}
	public void AutoLogin()
	{
		BackendReturnObject BRO = Backend.BMember.LoginWithTheBackendToken();

		if(BRO.IsSuccess())
		{
			Debug.Log("자동 로그인 성공");
		}
		else
		{
			string error = BRO.GetStatusCode();

			switch (error)
			{
				case "GoneResourceException":
					Debug.Log("1년뒤 refresh_token이 만료된 경우");
					break;
				case "BadUnauthorizedException":
					Debug.Log("다른 기기로 로그인 하여 refresh_token이 만료된 경우");
					break;
				case "BadPlayer":
					Debug.Log("차단된 유저");
					break;
				default:
					Debug.Log("로그인 완료");
					break;
			}
		}
	}

}

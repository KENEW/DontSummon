using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreen : MonoSingleton<LoginScreen>
{
	public GameObject TitleScreen;
	public GameObject loginScreen;
	public GameObject loginFailedPanel;


	public void NextScreen()
	{
		TitleScreen.SetActive(true);
		loginScreen.SetActive(false);

		SystemManager.Instance.isLoginCheck = true;
	}
	public void OnGoogleLogin()
	{
		SoundManager.Instance.PlaySFX("Button");

		BackEndFederationAuth.Instance.OnLogin();
	}
	public void OnGuestLogin()
	{
		SoundManager.Instance.PlaySFX("Button");

		MyData.Instance.LoadGameData();
		NextScreen();
	}
	public void OpenLoginFailedPanel()
	{
		loginFailedPanel.SetActive(true);
	}
	public void CloseLoginFailedPanel()
	{
		loginFailedPanel.SetActive(false);
	}
}

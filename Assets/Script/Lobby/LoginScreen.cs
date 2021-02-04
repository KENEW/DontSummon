using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreen : MonoBehaviour
{
	public GameObject TitleScreen;
	public GameObject loginScreen;

	public void NextScreen()
	{
		TitleScreen.SetActive(true);
		loginScreen.SetActive(false);
	}
	public void OnGoogleLogin()
	{
		SoundManager.Instance.PlaySFX("Button");

		BackEndFederationAuth.Instance.OnClickGoogleServer();
		NextScreen();
	}
	public void OnGuestLogin()
	{
		SoundManager.Instance.PlaySFX("Button");

		MyData.Instance.LoadGameData();
		NextScreen();
	}
}

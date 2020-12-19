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
		BackEndFederationAuth.Instance.OnClickGoogleServer();
		NextScreen();
	}
	public void OnTestLogin()
	{
		BackEndAuthentication.Instance.OneClickCustomServer();
		NextScreen();
	}
	public void OnGuestLogin()
	{
		//BackEndAuthentication.Instance.OneClickCustomServer();
		MyData.Instance.LoadData();
		NextScreen();
	}
	private void Update()
	{
		//if(BackEndFederationAuth.Instance.isLoginCheck == true)
		//{
		//	TitleScreen.SetActive(true);
		//	loginScreen.SetActive(false);
		//}
	}
}

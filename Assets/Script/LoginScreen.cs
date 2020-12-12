using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreen : MonoBehaviour
{
	public GameObject TitleScreen;
	public GameObject loginScreen;
	public void OnGuesePlayLogin()
	{
		TitleScreen.SetActive(true);
		loginScreen.SetActive(false);
	}

	public void OnGoogleLogin()
	{
		BackEndFederationAuth.Instance.OnLogin();
	}

	private void Update()
	{
		if(BackEndFederationAuth.Instance.isLoginCheck == true)
		{
			TitleScreen.SetActive(true);
			loginScreen.SetActive(false);
		}
	}
}

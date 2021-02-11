using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
	public GameObject firstScreen;
	public GameObject titleScreen;

	private void Start()
	{
		Time.timeScale = 1.0f;

		if (!SystemManager.Instance.isLoginCheck)
		{
			firstScreen.SetActive(true);
			SystemManager.Instance.isLoginCheck = true;
		}
		else
		{
			titleScreen.SetActive(true);
		}
	}
}

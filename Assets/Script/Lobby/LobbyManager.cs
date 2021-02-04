using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
	public GameObject firstScreen;
	public GameObject titleScreen;

	public GameManager gameManager;

	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		Time.timeScale = 1.0f;

		if (!gameManager.isLoginCheck)
		{
			firstScreen.SetActive(true);
			gameManager.isLoginCheck = true;
		}
		else
		{
			titleScreen.SetActive(true);
		}
	}
}

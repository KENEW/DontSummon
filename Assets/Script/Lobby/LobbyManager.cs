using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
	public GameManager gameManager;

	public GameObject firstScreen;
	public GameObject titleScreen;

	private void Start()
	{
		Time.timeScale = 1.0f;

		gameManager = FindObjectOfType<GameManager>();

		if (gameManager.isFirstCheck)
		{
			firstScreen.SetActive(true);
			gameManager.isFirstCheck = false;
		}
		else
		{
			titleScreen.SetActive(true);
		}
	}
}

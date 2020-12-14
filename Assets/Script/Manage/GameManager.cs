using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Awake()
	{
		ScreenSetting();
	}
	public void ScreenSetting()
	{
		Screen.SetResolution(720, 1280, false);
		//PlayerPrefs.SetInt("Screenmanager Resolution Width", 720);
		//PlayerPrefs.SetInt("Screenmanager Resolution Height", 1280);
		//PlayerPrefs.Save();
	}
}

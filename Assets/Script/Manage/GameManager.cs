using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	public bool isLoginCheck = false;
	public bool isGPSCheck = false;

	private void Awake()
	{
		ScreenSetting();
	}
	public void ScreenSetting()
	{
		Screen.SetResolution(1080, 1920, false);
	}
}

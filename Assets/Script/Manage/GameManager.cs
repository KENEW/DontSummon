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
		Debug.Log("해상도 세팅");

		Camera camera = Camera.main;
		Rect rect = camera.rect;

		float scaleHeight = ((float)Screen.width / Screen.height) / ((float)9 / 16);  //(가로 / 세로);
		float scaleWidth = 1.0f / scaleHeight;

		if(scaleHeight < 1.0f)
		{
			rect.height = scaleHeight;
			rect.y = (1.0f - scaleHeight) / 2.0f;
		}
		else
		{
			rect.width = scaleWidth;
			rect.x = (1.0f - scaleWidth) / 2.0f;
		}

		camera.rect = rect;

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
}

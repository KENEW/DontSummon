using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Awake()
	{
		ScreenSetting();
	}
	private void Start()
	{
		MyData.Instance.SaveData();
	}
	public void ScreenSetting()
	{
		Screen.SetResolution(720, 1280, false);
	}
}

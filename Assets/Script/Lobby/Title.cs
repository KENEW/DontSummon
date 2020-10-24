using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
	public GameObject StageSelect;
	public GameObject TitleScreen;

	public GameObject CreditPanel;

	public void StartButton()
	{
		StageSelect.SetActive(true);
		TitleScreen.SetActive(false);
	}
	public void CreditButton()
	{
		CreditPanel.SetActive(!CreditPanel.activeSelf);
	}
	public void ExitButton()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
	public Animator logoAnimator;

	public GameObject StageSelect;
	public GameObject TitleScreen;
	public GameObject CreditPanel;

	private bool isBGMCheck = false;

	private void Start()
	{
		SoundManager.Instance.PlayBGM("Title");
	}
	public void StartButton()
	{
		SoundManager.Instance.PlaySFX("Button");

		MyData.Instance.DataInit();

		StageSelect.SetActive(true);
		TitleScreen.SetActive(false);
	}
	public void CreditButton()
	{
		SoundManager.Instance.PlaySFX("Button");

		CreditPanel.SetActive(!CreditPanel.activeSelf);
	}
	public void ExitButton()
	{
		SoundManager.Instance.PlaySFX("Button");

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}

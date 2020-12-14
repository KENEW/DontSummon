using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
	public Animator logoAnimator;

	public GameObject StageSelect;
	public GameObject TitleScreen;
	public GameObject CreditPanel;

	private void Start()
	{
		logoAnimator.SetTrigger("LogoStart");
	}
	public void StartButton()
	{
		SoundManager.Instance.PlaySFX("Button");

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

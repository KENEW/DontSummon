using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
	public Animator logoAnimator;

	public GameObject StageSelect;
	public GameObject TitleScreen;
	public GameObject CreditPanel;

	public Button rankingButton;

	private void Start()
	{
		SoundManager.Instance.PlayBGM("Title");
	}
	public void StartButton()
	{
		SoundManager.Instance.PlaySFX("Button");

		BackEndFederationAuth.Instance.OnAddAchievement("Start");
		MyData.Instance.DataInit();

		StageSelect.SetActive(true);
		TitleScreen.SetActive(false);
	}
	public void CreditButton()
	{
		SoundManager.Instance.PlaySFX("Button");

		CreditPanel.SetActive(!CreditPanel.activeSelf);
	}
	private void OnEnable()
	{
		rankingButton.interactable = SystemManager.Instance.isGPSCheck ? true : false;
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadScene : MonoSingleton<LoadScene>
{
	public GameObject loadingPanel;
	public Image curLoadGuageBar;
	public Image sceneScreen;
	public Text curLoadText;

	private void Start()
	{
		if(loadingPanel.active == true)
		{
			Debug.Log("로딩패널을 꺼주세요");
			loadingPanel.SetActive(false);
		}

	}
	public void LoadStart(string sceneName)
	{
		loadingPanel.SetActive(true);
		StartCoroutine(Loading(sceneName));
	}
	IEnumerator Loading(string sceneName)
	{
		loadingPanel.SetActive(true);

		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		Debug.Log("병합테스트");
		operation.allowSceneActivation = false;

		float timer = 0;

		while (!operation.isDone)
		{
			yield return null;

			if(operation.progress < 0.9f)
			{
				curLoadGuageBar.fillAmount = operation.progress;
			}
			else
			{
				timer += Time.deltaTime;
				curLoadGuageBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
				curLoadText.text = (int)(curLoadGuageBar.fillAmount * 100f)+ "";

				if (curLoadGuageBar.fillAmount >= 1.0f)
				{
					sceneScreen.DOFade(1, 1.0f).OnComplete(() => { operation.allowSceneActivation = true; });
					yield break;
				}
			}
		}
	}
}

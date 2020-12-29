using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LightLoading : MonoBehaviour
{
	public GameObject loadingPanel;
	public Image fadeScreen;
	public Image curLoadGuageBar;

	public void LoadStart(string sceneName)
	{
		Time.timeScale = 1;
		Debug.Log("타임스케일" + Time.timeScale);
		loadingPanel.SetActive(true);
		StartCoroutine(Loading(sceneName));
	}
	IEnumerator Loading(string sceneName)
	{
		loadingPanel.SetActive(true);

		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		operation.allowSceneActivation = false;

		float timer = 0;

		while (!operation.isDone)
		{
			yield return null;

			if (operation.progress < 0.9f)
			{
				curLoadGuageBar.fillAmount = operation.progress;
			}
			else
			{
				timer += Time.deltaTime;
				curLoadGuageBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

				if (curLoadGuageBar.fillAmount >= 1.0f)
				{
					fadeScreen.DOFade(1, 1.0f).OnComplete(() => { operation.allowSceneActivation = true; });
					yield break;
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	public Text curLoadText;
	public Image curLoadGuageBar;

	public Image sceneScreen;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.O))
		{
			LoadStart();
		}
	}
	public void LoadStart()
	{
		StartCoroutine(Loading());
	}
	
	IEnumerator Loading()
	{
		Debug.Log("로딩 시작");
		AsyncOperation operation = SceneManager.LoadSceneAsync("EditScene");
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
					operation.allowSceneActivation = true;
					yield break;
				}
			}
		}

		Debug.Log("로딩 끝");

		sceneScreen.DOFade(1, 1.0f);
	}
}

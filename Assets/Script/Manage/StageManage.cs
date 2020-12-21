using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageManage : MonoSingleton<StageManage>
{

	public GameObject ClearObj;
	public GameObject FailedObj;

	public GameObject ClearScreen;
	public GameObject FailedScreen;

	public GameObject StartPanel;
	public GameObject PauseBtn;

	public Animator animator;

	public bool gameState = false;
	public int curStage;

	private void Start()
	{
		Time.timeScale = 0;
		curStage = 1;
		StartCoroutine("GameStart");
		StageInit();
	}
	public void GameStateTrue()
    {
		gameState = true;
    }
	public void GameStateFalse()
    {
		gameState = false;
    }

	IEnumerator GameStart()
	{
		//ReadyObj.SetActive(true);
		//yield return new WaitForSecondsRealtime(2f);
		//ReadyObj.SetActive(false);
		//StartObj.SetActive(true);
		////StartPanel.SetActive(true);
		yield return new WaitForSecondsRealtime(0.2f);
		animator.SetTrigger("Start");

		yield return new WaitForSecondsRealtime(4.5f);
		//StartObj.SetActive(false);

		StartPanel.gameObject.transform.DOMoveY(1.0f, 3f).OnComplete(() => { StartPanel.SetActive(false);});
		Time.timeScale = 1;

		PauseBtn.SetActive(true);
	}


	private void StageInit()
	{
		switch (curStage)
		{
			case 1:
				//SceneManager.LoadScene("Stage1");
				break;
			case 2:
				//SceneManager.LoadScene("Stage2");
				break;
			case 3:
				//SceneManager.LoadScene("Stage3");
				break;
			case 4:
				//SceneManager.LoadScene("Stage4");
				break;
			case 5:
				//SceneManager.LoadScene("Stage5");
				break;
		}

	}



	public void StageClear()
	{
		//SoundManager.instance.PlaySFX("Clear");
		//gameState = false;
		//StartCoroutine("GameEnd");
		//SceneManager.LoadScene("Lobby");
		//Invoke("LoadNextScene", 2f);
		//StartCoroutine("LoadNextScene");

		StartCoroutine(Clear());
	}

	public void StageFailed()
	{
		//SoundManager.instance.PlaySFX("Failed");
		//gameState = false;
		//StartCoroutine("GameEnd");
		//Invoke("LoadSceneLobby",2f);
		//StartCoroutine("LoadSceneLobby");
		//yield return new WaitForSeconds(2.0f);
		//SceneManager.LoadScene("TestScene");

		StartCoroutine(GameOver());
	}

	IEnumerator Clear()
    {
		Time.timeScale = 0;
		ClearObj.SetActive(true);
		yield return new WaitForSecondsRealtime(3f);
		ClearObj.SetActive(false);
		ClearScreen.SetActive(true);
		yield return null;
    }

	IEnumerator GameOver()
    {
		Time.timeScale = 0;
		FailedObj.SetActive(true);
		yield return new WaitForSecondsRealtime(3f);
		FailedObj.SetActive(false);
		FailedScreen.SetActive(true);
		yield return null;
	}

	IEnumerator LoadSceneLobby()
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(4f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		//SceneManager.LoadScene("Lobby");
	}
	public void LoadSceneRestart()
	{
		SceneManager.LoadScene("editScene");
	}

	IEnumerator LoadNextScene()
    {
		Time.timeScale = 0;
		//점수 계산
		yield return new WaitForSecondsRealtime(4f);
	
		Scene scene = SceneManager.GetActiveScene();
		int curScene = scene.buildIndex;
		int nextScene = curScene + 1;
		SceneManager.LoadScene("Stage" + nextScene);
	}

}

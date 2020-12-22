using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageManage : MonoSingleton<StageManage>
{
	public Animator animator;

	public GameObject ClearObj;
	public GameObject FailedObj;

	public GameObject ClearScreen;
	public GameObject FailedScreen;

	public GameObject StartPanel;
	public GameObject PauseBtn;

	public Text stageText;
	public Text chapterText;

	public bool gameState = false;
	public bool playerGuard = false;

	public int curStage;

	private void Start()
	{
		Time.timeScale = 0;
		curStage = 1;
		StartCoroutine("GameStart");
		StageInit();

		chapterText.text =  MyData.Instance.stageInfo.curChapter + "";
		stageText.text =	MyData.Instance.stageInfo.curStage + "" ;
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
		yield return new WaitForSecondsRealtime(0.4f);
		animator.SetTrigger("Start");

		yield return new WaitForSecondsRealtime(6.3f);

		StartPanel.gameObject.transform.DOMoveY(1.0f, 3f).OnComplete(() => { StartPanel.SetActive(false);});	//Delay CallBack
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



	public void OnStageClear()
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
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Y))
		{
			OnStageClear();
		}
	}

	IEnumerator Clear()
    {
		playerGuard = true;
		Time.timeScale = 1;
		ClearObj.SetActive(true);
		yield return new WaitForSecondsRealtime(3f);
		ClearObj.SetActive(false);

		ClearScreen.SetActive(true);
		Debug.Log(StageClear.Instance);


		if(MyData.Instance.stageInfo.curStage >= 4)
		{
			GameResult.Instance.GetRemainTime(TimeLimit.Instance.GetClearTime());
			GameResult.Instance.GetRemainHealth(PlayerHp.Instance.GetHp());
			GameResult.instance.GetaquireScore(Score.Instance.GetScore());
			GameResult.Instance.ScoreResult();
		}
		else
		{
			StageClear.Instance.GetRemainTime(TimeLimit.Instance.GetClearTime());
			StageClear.Instance.GetRemainHealth(PlayerHp.Instance.GetHp());
			StageClear.instance.GetaquireScore(Score.Instance.GetScore());
			StageClear.Instance.ScoreResult();
		}

		MyData.Instance.stageInfo.curStage++;

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

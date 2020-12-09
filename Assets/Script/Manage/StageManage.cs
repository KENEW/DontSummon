using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageManage : MonoSingleton<StageManage>
{
	public NeedMonster needMonster;

	public GameObject ClearObj;
	public GameObject FailedObj;

	public bool gameState = false;
	public int curStage;

	private void Start()
	{
		//Debug.Log(SoundManager.instance);
		curStage = 1;
		Invoke("GameStateTrue", 2f);
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
		gameState = false;
		ClearObj.SetActive(true);
		//Invoke("LoadSceneLobby", 2f);
		//yield return new WaitForSeconds(2.0f);
		//SceneManager.LoadScene("Lobby");
		Invoke("LoadNextScene", 2f);
		
	}

	public void StageFailed()
	{
		//SoundManager.instance.PlaySFX("Failed");
		gameState = false;
		FailedObj.SetActive(true);
		Invoke("LoadSceneLobby", 2f);

		//yield return new WaitForSeconds(2.0f);
		//SceneManager.LoadScene("TestScene");
	}

	public void LoadSceneLobby()
	{
		SceneManager.LoadScene("Lobby");
	}
	public void LoadSceneRestart()
	{
		SceneManager.LoadScene("editScene");
	}

	public void LoadNextScene()
    {
		Scene scene = SceneManager.GetActiveScene();
		int curScene = scene.buildIndex;
		int nextScene = curScene + 1;
		SceneManager.LoadScene(nextScene);
	}

}

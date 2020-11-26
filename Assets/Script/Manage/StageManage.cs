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
				//needMonster.monsterFlag = 0;
				//needMonster.curImage.sprite = needMonster.monsterSprite[needMonster.monsterFlag];
				//needMonster.needNum = 3;
				//MonsterGenerator.instance.MosnterCreate();
				break;
			case 2:
				//needMonster.monsterFlag = 1;
				//needMonster.curImage.sprite = needMonster.monsterSprite[needMonster.monsterFlag];
				//needMonster.needNum = 2;
				//MonsterGenerator.instance.MosnterCreate();

				break;
			case 3:
				//needMonster.monsterFlag = 2;
				//needMonster.curImage.sprite = needMonster.monsterSprite[needMonster.monsterFlag];
				//needMonster.needNum = 1;
				//MonsterGenerator.instance.MosnterCreate();

				break;
		}

	}



	public void StageClear()
	{
		//SoundManager.instance.PlaySFX("Clear");
		gameState = false;
		ClearObj.SetActive(true);
		Invoke("LoadSceneLobby", 2f);
		//yield return new WaitForSeconds(2.0f);
		//SceneManager.LoadScene("Lobby");
	}

	public void StageFailed()
	{
		//SoundManager.instance.PlaySFX("Failed");
		gameState = false;
		FailedObj.SetActive(true);
		Invoke("LoadSceneRestart", 2f);

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

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManage : MonoSingleton<StageManage>
{
	public NeedMonster needMonster;

	public GameObject ClearObj;
	public GameObject FailedObj;

	public bool gameState = false;
	private int curStage
	{
		get
		{
			return curStage;
		}
		set
		{
			curStage = value;
		}
	}

	private void Start()
	{
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
				needMonster.monsterFlag = 0;
				needMonster.curImage.sprite = needMonster.monsterSprite[needMonster.monsterFlag];
				needMonster.needNum = 3;
				MonsterGenerator.instance.MosnterCreate();
				break;
			case 2:
				needMonster.monsterFlag = 1;
				needMonster.curImage.sprite = needMonster.monsterSprite[needMonster.monsterFlag];
				needMonster.needNum = 2;
				MonsterGenerator.instance.MosnterCreate();

				break;
			case 3:
				needMonster.monsterFlag = 2;
				needMonster.curImage.sprite = needMonster.monsterSprite[needMonster.monsterFlag];
				needMonster.needNum = 1;
				MonsterGenerator.instance.MosnterCreate();

				break;
		}

	}

	public void StaggClear()
	{
		if (curStage >= 3)
		{
			//로비로 이동
		}
		else
		{
			ClearObj.SetActive(true);
<<<<<<< HEAD

			//Invoke("PlayerInit", 3.0f);
=======
			Invoke("StageInit", 2.0f);
>>>>>>> d60ae03f2237c6991ddf39322753c4a27a3b3a3e
		}
	}
	public void StageFailed()
	{
		gameState = false;
		FailedObj.SetActive(true);
	}

}

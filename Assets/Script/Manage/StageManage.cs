using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManage : MonoSingleton<StageManage>
{
	public NeedMonster needMonster;
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
	}

	private void StageInit()
	{
		switch (curStage)
		{
			case 1:
				
				break;
			case 2:

				break;
			case 3:

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
			Invoke("PlayerInit", 3.0f);
		}
	}
	public void StageFailed()
	{
		Debug.Log("스테이지 실패");
		//로비로 이동
	}

}

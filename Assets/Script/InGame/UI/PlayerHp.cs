using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoSingleton<PlayerHp>
{
	public GameObject[] hpObject;

	[SerializeField] 
	private int maxHp;
	private int curHp;

	private void Start()
	{
		Init();
		UIUpdate();
	}
	public void Init()
	{
		curHp = MyData.Instance.stageInfo.curHp;
	}
	public void RecoveryHp(int hpValue)
	{
		if(curHp + hpValue >= maxHp)
		{
			curHp = maxHp;
		}
		else
		{
			curHp += hpValue;
		}

		UIUpdate();
	}
	public void GetDamage(int hpValue)
	{
		if(StageManage.Instance.GetGameState() == GameState.Play)
		{
			if (curHp - hpValue <= 0)
			{
				curHp = 0;
				StageManage.Instance.StateChange(GameState.Failed);
			}
			else
			{
				curHp -= hpValue;
			}
		}

		UIUpdate();
	}
	private void UIUpdate()
	{
		for(int i = 0; i < curHp; i++)
		{
			hpObject[i].SetActive(true);
		}
		for(int i = curHp; i < maxHp; i++)
		{
			hpObject[i].SetActive(false);
		}
	}
	public int GetHp()
	{
		return curHp;
	}
}

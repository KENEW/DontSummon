using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoSingleton<PlayerHp>
{
	public GameObject[] hpObject;

	//[SerializeField]
	//private int curHp;
	[SerializeField]
	private int maxHp;

	public int curHp;

	//NeedMonster needMonster;

	private void Start()
	{
		//needMonster = GameObject.Find("NeedMonster").GetComponent<NeedMonster>();

		//Init();
		GetHp();
		DrawHp();
	}

	public void Init()
	{
		maxHp = 3;
		curHp = maxHp;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			RecoveryHp(1);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			GetDamage(1);
		}

		if(curHp==0) //현재 체력이 0이면
        {
			StageManage.Instance.StageFailed();
        }

        /*else
        {
			if(needMonster.needNum==0)
            {
				Clear();
            }
        }*/

		DrawHp();
	}

	public void RecoveryHp(int hpValue) //회복
	{
		if(curHp + hpValue >= maxHp)
		{
			curHp = maxHp;
		}
		else
		{
			curHp += hpValue;
		}

		//DrawHp();
	}

	public void GetDamage(int hpValue) //데미지
	{
		if(!StageManage.Instance.playerGuard)
		{
			if (curHp - hpValue <= 0)
			{
				curHp = 0;
			}
			else
			{
				curHp -= hpValue;
			}
		}
	}

	private void DrawHp()
	{
		for(int i = 0; i < curHp; i++)
		{
			hpObject[i].SetActive(true);
			//animator.SetTrigger("HpAnime");
		}
		for(int i = curHp; i < maxHp; i++)
		{
			hpObject[i].SetActive(false);
			//animator.SetFloat("Reverse", -1);
			//animator.SetTrigger("HpAnime");

		}
	}

	public int GetHp()
	{
		return curHp;
	}

	public void Clear() //스테이지 클리어
	{
		Debug.Log("clear");
		StageManage.Instance.OnStageClear();

	}

}

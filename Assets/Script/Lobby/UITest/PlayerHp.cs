using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
	public GameObject[] hpObject;

	[SerializeField]
	private int curHp;
	[SerializeField]
	private int maxHp;

	private void Start()
	{
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
		if (curHp - hpValue <= 0)
		{
			curHp = 0;
		}
		else
		{
			curHp -= hpValue;
		}

		//DrawHp();
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
}

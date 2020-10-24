using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NeedMonster : MonoBehaviour
{
	public Sprite[] monsterSprite;
	public Image curImage;
	public Text needText;
	public int monsterFlag; //몬스터 구별 플래그 0-소 1-중 2-대

	public int needNum;

	PlayerHp playerHp;

	public enum Monster 
	{ 
		index = 0
	};

	Monster monster;

	void Start()
    {
		playerHp = GameObject.Find("PlayerHp").GetComponent<PlayerHp>();
	}

	void Update()
    {
		needText.text = needNum+"";

		
	
    }

	public void Success()
	{
		needNum -= 1;
	}

	public void Fail()
	{
		//라이프가 깎임
		playerHp.GetDamage(1);
	}

	public void Clear() //스테이지 클리어
    {
		Debug.Log("clear");
	}

	public void SetNeedText(int value)
	{
		needText.text = value + "";
	}

	public void SetNeedImage(Monster monster)
	{
		//curImage.sprite = monsterSprite[Monster];
	}
}

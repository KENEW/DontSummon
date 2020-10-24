using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NeedMonster : MonoBehaviour
{
	public Sprite[] monsterSprite;
	public Image curImage;
	public Text needText;

	public enum Monster 
	{ 
		index = 0
	};

	Monster monster;

	public void SetNeedText(int value)
	{
		needText.text = value + "";
	}

	public void SetNeedImage(Monster monster)
	{
		//curImage.sprite = monsterSprite[Monster];
	}
}

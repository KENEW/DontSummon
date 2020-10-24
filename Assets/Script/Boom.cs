using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
	private int curBoom;
	private float moveSpeed = 1.5f;

	private Vector3 centerPos = new Vector3(0.4f, -1f, 0);

	private string[] tempTag = new string[3] { "Small", "Medium", "Large" };

	private void Start()
	{
		Init();
		SoundManager.Instance.PlayBGM("Chapter1");
	}
	public void Init()
	{
		curBoom = 1;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			BoomFire();
		}
	}
	public void BoomFire()
	{
		if(curBoom > 0)
		{
			for(int a = 0; a < 3; a++)
			{
				//GameObject[] allBall = GameObject.FindGameObjectsWithTag("Monster");
				//List<GameObject> allBall = GameObject.FindGameObjectsWithTag("Monster");
				GameObject[] smallBall = GameObject.FindGameObjectsWithTag(tempTag[a]);


				for (int i = 0; i < smallBall.Length; i++)
				{
					Vector2 dir = centerPos - smallBall[i].transform.position;
					dir.Normalize();
					smallBall[i].GetComponent<Monster>().rigid.velocity = dir * moveSpeed;

				}
			}
			
		}
	}
}

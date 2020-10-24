using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
	private int curBoom;
	private float moveSpeed = 1.5f;
	public void Init()
	{
		curBoom = 1;
	}

	public void BoomFire()
	{
		if(curBoom > 0)
		{
			GameObject[] allBall = GameObject.FindGameObjectsWithTag("Monster");

			for (int i = 0; i < allBall.Length; i++)
			{
				Vector2 dir = new Vector3(0f, 0f, 0f) - allBall[i].transform.position;
				dir.Normalize();
				allBall[i].GetComponent<Ball>().rigid.velocity = dir * moveSpeed;

			}
		}
	}
}

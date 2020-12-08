using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using TheBackend;

public class Ranking : MonoBehaviour
{
	private void onClickGetUserInfo()
	{
		BackendReturnObject BRO = Backend.BMember.GetUserInfo();

		if(BRO.IsSuccess())
		{
			Debug.Log(BRO.GetReturnValue());
		}
		else
		{
			Debug.Log("서버 공동 에러 발생: " + BRO.GetErrorCode());
		}
	}
	private void Start()
	{
		Backend.Initialize(() =>
		{
			// 초기화 성공한 경우 실행
			if (Backend.IsInitialized)
			{
				// example
				// 버전체크 -> 업데이트
				Debug.Log("서버 초기화");

			}
			// 초기화 실패한 경우 실행
			else
			{

			}
		});
	}
}

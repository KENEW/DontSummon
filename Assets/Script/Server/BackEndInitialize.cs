using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackEndInitialize : MonoBehaviour
{
	private void Start()
    { 
	    Backend.Initialize(() =>
        {
            // 성공
            if (Backend.IsInitialized)
            {
                // 해쉬키 
                Debug.Log("구글 해쉬키 : " + Backend.Utils.GetGoogleHash());
            }
            // 실패
            else
            {
                Debug.LogError("초기화 실패");
            }
        });
    }
}
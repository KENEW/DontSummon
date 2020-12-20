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
            if (Backend.IsInitialized)
            {
                //Debug.Log("구글 해쉬키 : " + Backend.Utils.GetGoogleHash());
            }
            else
            {
                //Debug.LogError("초기화 실패");
            }
        });
    }
}
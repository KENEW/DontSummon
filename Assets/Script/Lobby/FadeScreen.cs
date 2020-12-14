
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
//using UnityEngine;

//public class FadeScreen : MonoSingleton<FadeScreen>
//{
//	public GameObject fadePanel;
//	public Image fadeImg;

//	public void FadeOut()
//	{
		
//	}
//	public void FadeIn()
//	{

//	}

//	IEnumerator fadeCoroutine()
//	{
//		Color color = fadeImg.color;

//		while (color.a <= 0.99f)
//		{
//			Debug.Log(color.a);
//			color.a += 0.03f;
//			fadeImg.color = color;
//			yield return null;
//		}

//		color.a = 1.0f;
//		fadeImg.color = color;

//		yield return new WaitForSeconds(2.0f);

//		loginScreen.SetActive(true);
//		gameObject.SetActive(false);
//	}
//}

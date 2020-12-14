using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoTitle : MonoBehaviour
{
	public GameObject loginScreen;
	public Image maskPanel;

	private void Start()
	{
		StartCoroutine(fadeCoroutine());
	}
	IEnumerator fadeCoroutine()
	{
		Color color = maskPanel.color;

		while (color.a <= 0.99f)
		{
			color.a += 0.03f;
			maskPanel.color = color;
			yield return null;
		}

		color.a = 1.0f;
		maskPanel.color = color;

		yield return new WaitForSeconds(2.0f);

		loginScreen.SetActive(true);
		gameObject.SetActive(false);
	}
}

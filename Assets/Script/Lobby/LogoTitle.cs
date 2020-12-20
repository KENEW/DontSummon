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
		StartCoroutine(fadeStart());
	}
	private IEnumerator fadeStart()
	{
		Color color = maskPanel.color;

		yield return new WaitForSeconds(0.5f);

		while (color.a >= 0.01f)
		{
			color.a -= 0.03f;
			maskPanel.color = color;
			yield return null;
		}

		yield return new WaitForSeconds(1.5f);

		while (color.a <= 0.99f)
		{
			color.a += 0.03f;
			maskPanel.color = color;
			yield return null;
		}

		color.a = 1.0f;
		maskPanel.color = color;
		yield return new WaitForSeconds(0.5f);

		loginScreen.SetActive(true);
		gameObject.SetActive(false);
	}
}

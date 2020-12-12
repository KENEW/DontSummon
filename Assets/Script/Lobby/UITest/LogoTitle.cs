using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoTitle : MonoBehaviour
{
	public GameObject loginScreen;
	public Image maskPanel;

	private void Start()
	{
		Screen.SetResolution(1080, 1920, false);

		StartCoroutine(FadeLogo());
	}

	IEnumerator FadeLogo()
	{
		SoundManager.Instance.PlaySFX("Logo");
		Color color = maskPanel.color;

		while(color.a <= 0.99f)
		{
			Debug.Log(color.a);
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

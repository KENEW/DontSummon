using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StoryScene : MonoBehaviour
{
	public GameObject mainTitle;

	public void NextButton()
	{
		SoundManager.Instance.PlaySFX("Button");
		mainTitle.SetActive(true);
		this.gameObject.SetActive(false);
	}
}

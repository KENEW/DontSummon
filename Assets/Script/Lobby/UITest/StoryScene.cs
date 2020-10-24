using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StoryScene : MonoBehaviour
{
	public GameObject[] storyPanel;
	public GameObject mainTitle;

	private int curPage = 0;

	public void NextButton()
	{
		SoundManager.Instance.PlaySFX("Button");
		storyPanel[curPage].SetActive(false);
		curPage++;

		if (curPage >= storyPanel.Length)
		{
			mainTitle.SetActive(true);
			gameObject.SetActive(false);
			return;
		}

		storyPanel[curPage].SetActive(true);
	}
}

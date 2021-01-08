using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoSingleton<TimeLimit>
{
	public Image hpGauge;
	public Image hpGaugeBack;
	public Text curTimeText;

	[SerializeField]
	private int maxTime;
	private int curTime;

	private Coroutine regularCo;

	private void Start()
	{
		TimeInit();
		regularCo = StartCoroutine(Regular());
	}
	public void TimeInit()
	{
		curTime = maxTime;
		curTimeText.text = maxTime.ToString();
	}
	IEnumerator Regular()
	{
		while(curTime > -1.0f)
		{
			yield return new WaitForSeconds(1f);
			curTime -= 1;

			if (curTime < (int)(maxTime * 0.2f))
			{
				curTimeText.color = Color.red;
			}
			else if(curTime < (int)(maxTime * 0.4f))
			{
				curTimeText.color = Color.yellow;
			}
			
			UIUpdate();
		}

		StageManage.Instance.StateChange(GameState.Failed);
	}
	private void UIUpdate()
	{
		//hpGauge.fillAmount = Mathf.Lerp(hpGauge.fillAmount, ((float)curTime / (float)maxTime), 7.5f * Time.deltaTime);
		hpGauge.fillAmount = (float)curTime / (float)maxTime;
		curTimeText.text = curTime.ToString();
	}
	public void StopRegular()
	{
		StopCoroutine(regularCo);
	}
	public int GetClearTime()
    {
		return (int)curTime;
    }
}

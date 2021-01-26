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

	public Color NORMAL_TIME;
	public Color WARNING1_TIME;
	public Color WARNING2_TIME;

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
		while(curTime >= 0f)
		{
			yield return new WaitForSeconds(1f);
			curTime -= 1;

			if (curTime < (int)(maxTime * 0.2f))
			{
				curTimeText.color = WARNING2_TIME;
			}
			else if(curTime < (int)(maxTime * 0.4f))
			{
				curTimeText.color = WARNING1_TIME;
			}
			else
			{
				curTimeText.color = NORMAL_TIME;
			}
			
			UIUpdate();
		}

		StageManage.Instance.StateChange(GameState.Failed);
	}
	private void UIUpdate()
	{
		hpGauge.fillAmount = (float)curTime / (float)maxTime;
		curTimeText.text = curTime.ToString();

		if(curTime==-1)
        {
			curTimeText.text = "0";
        }
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

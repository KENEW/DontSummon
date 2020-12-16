using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoSingleton<TimeLimit>
{
	public Animator animator;
	public Image timeGauge;
	public Text curTimeText;

	private float curTime;
	private float maxTime;
	private bool timeWarningCheck = false;

	public int clearTime;

	private Color WARNING_YELLOW = new Color(166 / 255f, 105 / 255f, 37 / 255f);
	private Color WARNING_RED = new Color(169 / 255f, 22 / 255f, 19 / 255f);

	private void Start()
	{
		SetStageTime(30);
		StartCoroutine(Regular());
	}
	private void Update()
	{
		timeGauge.fillAmount = Mathf.Lerp(timeGauge.fillAmount, ((float)curTime / (float)maxTime), 7.5f * Time.deltaTime);

		if(curTime <= (int)(maxTime * 0.2f))	//현재 시간이 전체 시간에 20% 아래로 내려갈시
		{
			timeGauge.color = WARNING_RED;
			timeWarningCheck = true;
		}
		else if (curTime <= (int)(maxTime * 0.4f))	//40%
		{
			timeGauge.color = WARNING_YELLOW;
		}
		else
		{
			timeGauge.color = Color.white;
			timeWarningCheck = false;
		}

		if (curTime <= 0) //현재 타임이 0이 되면
        {
			timeGauge.fillAmount = 0f;
			StageManage.Instance.StageFailed();
		}
	}
	public void SetStageTime(int time)	//스테이지 시간 설정
	{
		maxTime = time;
		curTime = maxTime;
	}
	public void AddTime(int time)	//현재 시간 추가
	{
		if(curTime + time >= maxTime)
		{
			curTime = maxTime;
			return;
		}

		curTime += time;
	}
	public void ClearTime()
	{
		clearTime = (int)curTime;
	}
	IEnumerator Regular()	//일정시간 마다 시간 설정
	{
		while(curTime > 0.0f)
		{
			yield return new WaitForSeconds(1f);
			if(timeWarningCheck)
			{
				animator.SetTrigger("TimeWarning");
			}
			curTime -= 1f;
			curTimeText.text = (int)curTime + "";
		}
	}
}

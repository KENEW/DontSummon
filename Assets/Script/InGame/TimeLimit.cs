using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoSingleton<TimeLimit>
{
	public PlayerHp playerHp;

	public Image hpGauge;
	public Image hpGaugeBack;
	public Text curTimeText;

	[SerializeField]
	private float maxTime;
	private float curTime;
	public int clearTime;

	private Coroutine regularCo;

	private void Start()
	{
		playerHp = GameObject.Find("HpPanel").GetComponent<PlayerHp>();
		curTime = maxTime;
		regularCo = StartCoroutine(Regular());
	}
	private void Update()
	{
		hpGauge.fillAmount = Mathf.Lerp(hpGauge.fillAmount, ((float)curTime / (float)maxTime), 7.5f * Time.deltaTime);

		/*if (backHpDamage)
		{
			hpGaugeBack.fillAmount = Mathf.Lerp(hpGaugeBack.fillAmount, hpGauge.fillAmount, 3.5f * Time.deltaTime);

			if (hpGauge.fillAmount >= hpGaugeBack.fillAmount - 0.01f)
			{
				backHpDamage = false;
				hpGaugeBack.fillAmount = hpGauge.fillAmount;
			}
		}*/


		if (curTime == 0) //현재 타임이 0이 되면
        {
			playerHp.GetDamage(playerHp.curHp);

			StageManage.Instance.StageFailed();
		}
	}
	IEnumerator Regular()
	{
		while(curTime > 0.0f)
		{
			yield return new WaitForSeconds(1f);
			curTime -= 1f;
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

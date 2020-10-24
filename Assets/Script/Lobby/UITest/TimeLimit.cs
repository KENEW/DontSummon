using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
	public Image hpGauge;
	public Image hpGaugeBack;
	public Text curTimeText;

	private float curTime;
	private float maxTime;

	PlayerHp playerHp;

	private bool backHpDamage = false;

	private void Start()
	{
		maxTime = 20f;
		curTime = maxTime;
		StartCoroutine(Regular());

		playerHp = GameObject.Find("PlayerHp").GetComponent<PlayerHp>();
	}
	private void Update()
	{
		hpGauge.fillAmount = Mathf.Lerp(hpGauge.fillAmount, ((float)curTime / (float)maxTime), 7.5f * Time.deltaTime);

		if (backHpDamage)
		{
			hpGaugeBack.fillAmount = Mathf.Lerp(hpGaugeBack.fillAmount, hpGauge.fillAmount, 3.5f * Time.deltaTime);

			if (hpGauge.fillAmount >= hpGaugeBack.fillAmount - 0.01f)
			{
				backHpDamage = false;
				hpGaugeBack.fillAmount = hpGauge.fillAmount;
			}
		}

		if(curTime==0) //현재 타임이 0이 되면
        {
			playerHp.hpObject[0].SetActive(false); //Life 0
			playerHp.hpObject[1].SetActive(false);
			playerHp.hpObject[2].SetActive(false);
			
			GameOver();
        }
	}
	private void BackHpRun()
	{
		backHpDamage = true;
	}

	IEnumerator Regular()
	{
		while(curTime > 0.0f)
		{
			yield return new WaitForSeconds(1f);
			curTime -= 1f;
			curTimeText.text = (int)curTime + "";
			Invoke("BackHpRun", 0.1f);
		}
	}

	public void GameOver()
    {
		Debug.Log("game over");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameResult : MonoBehaviour
{
    public GameObject resultPanel;
    public GameObject fadePanel;

    public Text remainTimeText;
    public Text remainHealthText;
    public Text totalScoreText;

    private int remainTime = 0;
    private int remainHealth = 0;
    private int acquireScore = 0;
    private int totalScore = 0;

    private int SET_HEALTH_UP = 50;
    private int SET_RE_TIME_UP = 30;

	private void Update()
	{
        //Test
		if(Input.GetKeyDown(KeyCode.T))
        {
            GetaquireScore(5000);
            GetRemainTime(60);
            GetRemainHealth(3);

            ScoreResult();
        }
	}
    public void OnLobbyButton()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<Image>().DOFade(1.0f, 2.0f).OnComplete(() => 
        {
            LoadScene.Instance.LoadStart("Lobby");
        });
	}
	public void GetRemainTime(int time)
    {
        remainTime = time;
	}
    public void GetRemainHealth(int health)
    {
        remainHealth = health;
	}
    public void GetaquireScore(int score)
    {
        acquireScore = score;
    }
    private void InitScore()
    {
        remainTime = 0;
        remainHealth = 0;
        acquireScore = 0;
        totalScore = 0;
    }
    private void ScoreResult()
    {
        totalScore += remainHealth * SET_HEALTH_UP;
        totalScore += remainTime * SET_RE_TIME_UP;
        totalScore += acquireScore;

        StartCoroutine(CountSequence());
    }
    private IEnumerator CountSequence()
    {
        float delay = 0.4f;

        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    StartCoroutine(Count(remainTimeText, remainTime, 0));
                    yield return new WaitForSeconds(delay);
                    break;
                case 1:
                    StartCoroutine(Count(remainHealthText, remainHealth, 0));
                    yield return new WaitForSeconds(delay);
                    break;
                case 2:
                    StartCoroutine(Count(totalScoreText, totalScore, acquireScore));
                    yield return new WaitForSeconds(delay);
                    break;
            }
        }
    }
    private IEnumerator Count(Text countText, float target, float current)
    {
        float duration = 0.5f; // Duration Counting TIme
        float offset = (target - current) / duration;

		while (current < target)
		{
			current += offset * Time.deltaTime;
            countText.text = ((int)current).ToString();

			yield return null;
		}

        current = target;
        countText.text = ((int)current).ToString();
    }
}

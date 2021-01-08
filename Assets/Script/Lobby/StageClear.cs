using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StageClear : MonoSingleton<StageClear>
{
    public LightLoading lightLoading;

    public Text remainTimeText;
    public Text remainHealthText;
    public Text totalScoreText;

    private int remainTime = 0;
    private int remainHealth = 0;
    private int acquireScore = 0;
    private int totalScore = 0;

    private const int SET_HEALTH_UP = 0;
    private const int SET_RE_TIME_UP = 10;

    private void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.T))
        {
            ScoreResult(5000, 300, 200);
        }
    }
    private void OnEnable()
    {
        SoundManager.Instance.PlaySFX("StageResultSFX");
        SoundManager.Instance.PlayBGM("StageResultBGM");
    }
    public void OnNextStage()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlaySFX("Button");
        lightLoading.LoadStart(SceneManager.GetActiveScene().name);
    }
    private void InitScore()
    {
        remainTime = 0;
        remainHealth = 0;
        acquireScore = 0;
        totalScore = 0;
    }
    public void ScoreResult(int time, int health, int score)
    {
        remainTime = time;
        remainHealth = health;
        acquireScore = score;

        totalScore += remainHealth * SET_HEALTH_UP;
        totalScore += remainTime * SET_RE_TIME_UP;
        totalScore += acquireScore;

        MyData.Instance.stageInfo.curScore += totalScore;

        StartCoroutine(CountSequence());
    }
    private IEnumerator CountSequence()
    {
        float delay = 0.4f;

        for (int i = 0; i < 4; i++)
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

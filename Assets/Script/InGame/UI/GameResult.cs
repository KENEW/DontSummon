using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameResult : MonoSingleton<GameResult>
{
    public Animator highScoreTextAnimation;

    public LightLoading lightLoading;   

    public Text remainTimeText;
    public Text remainHealthText;
    public Text totalScoreText;
    public Text addScoreText;
    public Text highScoreText;

    private int remainTime = 0;
    private int remainHealth = 0;
    private int acquireScore = 0;
    private int addScore = 0;

    private int totalScore = 0;

    private int SET_HEALTH_UP = 50;
    private int SET_RE_TIME_UP = 10;

	private void OnEnable()
	{
        SoundManager.Instance.PlaySFX("StageResultSFX");
        SoundManager.Instance.PlayBGM("Stage1BGM");
    }
	private void Update()
	{
        //Test
		if(Input.GetKeyDown(KeyCode.T))
        {
            ScoreResult(500, 400, 300);
        }
	}
    public void OnLobby()
    {
        SoundManager.Instance.PlaySFX("Button");
        lightLoading.LoadStart("Lobby");
	}
    public void OnReStart()
    {
        SoundManager.Instance.PlaySFX("Button");
        MyData.Instance.scoreInfo = new ScoreInfo();

        lightLoading.LoadStart("Stage1");
    }
    public void OnRanking()
    {
        SoundManager.Instance.PlaySFX("Button");
        BackEndFederationAuth.Instance.OnShowLeaderBoard();
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

        highScoreText.text = MyData.Instance.scoreInfo.stageScore[MyData.Instance.stageInfo.curChapter - 1].ToString();

        addScore += remainHealth * SET_HEALTH_UP;
        addScore += remainTime * SET_RE_TIME_UP;

        totalScore = MyData.Instance.stageInfo.curScore + addScore + Score.Instance.GetScore();

        MyData.Instance.stageInfo.curScore += totalScore;

        if (totalScore > MyData.Instance.scoreInfo.stageScore[MyData.Instance.stageInfo.curChapter - 1])
        {
            BackEndFederationAuth.Instance.OnSetLeaderBoard(MyData.Instance.stageInfo.curChapter, totalScore);

            if(MyData.Instance.scoreInfo.stageScore[MyData.Instance.stageInfo.curChapter - 1] == 0)
            {
                BackEndFederationAuth.Instance.OnAddAchievement("Chapter" + MyData.Instance.stageInfo.curChapter);
			}

            MyData.Instance.scoreInfo.stageScore[MyData.Instance.stageInfo.curChapter - 1] = totalScore;
            MyData.Instance.SaveGameData();
            BackEndGameInfo.Instance.OnClickGameInfoUpdate();
        }

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
                    StartCoroutine(Count(addScoreText, addScore, 0));
                    yield return new WaitForSeconds(delay);
                    break;
                case 3:
                    StartCoroutine(Count(totalScoreText, totalScore, 0));
                    yield return new WaitForSeconds(delay);
                    break;
                case 4:
                    if(totalScore > MyData.Instance.scoreInfo.stageScore[MyData.Instance.stageInfo.curChapter])
                    {
                        StartCoroutine(Count(highScoreText, totalScore, MyData.Instance.scoreInfo.stageScore[MyData.Instance.stageInfo.curChapter]));
                    }
                    yield return new WaitForSeconds(delay);
                    highScoreTextAnimation.SetTrigger("HighScore");
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

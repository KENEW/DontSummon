using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoSingleton<Score>
{
	public Text scoreText;
	private int score;

	private void Start()
	{
		ScoreInit();
	}
	void Update()
    {
		ScoreUpdate();
    }
	public void ScoreInit()
	{
		score = MyData.Instance.stageInfo.curScore;
		ScoreUpdate();
	}
	public void SetScore(int score)
	{
		this.score = score;
		ScoreUpdate();
	}
	public int GetScore()
	{
		return score;
	}
	public void AddScore(int score)
	{
		this.score += score;
	}
	private void ScoreUpdate()
	{
		scoreText.text = score + "";
	}
}

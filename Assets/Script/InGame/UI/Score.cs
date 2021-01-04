using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoSingleton<Score>
{
	public Text scoreText;

	private int score;

	public void ScoreInit()
	{
		score = 0;
		ScoreUpdate();
	}
	public int GetScore()
	{
		return score;
	}
	public void AddScore(int score)
	{
		this.score += score;
		ScoreUpdate();
	}
	private void ScoreUpdate()
	{
		scoreText.text = (score + MyData.Instance.stageInfo.curScore) + "";
	}
}

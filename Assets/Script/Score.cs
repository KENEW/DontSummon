using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public Text scoreText;
	public int score;
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
		score = 0;
		ScoreUpdate();
	}
	public void SetScore(int score)
	{
		this.score = score;
		ScoreUpdate();
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

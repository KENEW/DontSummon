using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public enum GameState
{
	Ready,
	Play,
	Pause,
	Clear,
	Failed
}
	
public class StageManage : MonoSingleton<StageManage>
{
	public Animator animator;

	public GameObject readyScreen;
	public GameObject clearPanel;
	public GameObject clearScreen;
	public GameObject clearChapterPanel;
	public GameObject failedPanel;
	public GameObject failedScreen;

	public Image bgImg;
	public Sprite[] bgSpr;

	public Text stageText;
	public Text chapterText;

	private GameState gameState;

	public int curMonster = 0;

	private void Start()
	{
		StateChange(GameState.Ready);
	}
	private void Update()
	{
		//Test
		if (Input.GetKeyDown(KeyCode.Y))
		{
			StateChange(GameState.Clear);
		}
	}
	public void StateChange(GameState state)
	{
		switch (state)
		{
			case GameState.Ready:
				StageInit();
				Score.Instance.ScoreInit();
				StartCoroutine("GameReadyCo");
				break;
			case GameState.Play:
				Time.timeScale = 1;
				break;
			case GameState.Pause:
				Time.timeScale = 0;
				break;
			case GameState.Clear:
				MyData.Instance.stageInfo.curHp = PlayerHp.Instance.GetHp();
				StartCoroutine(Clear());
				break;
			case GameState.Failed:
				StartCoroutine(GameOver());
				break;
		}

		gameState = state;
	}
	public GameState GetGameState()
	{
		return gameState;
	}
	IEnumerator GameReadyCo()
	{
		Time.timeScale = 0;

		chapterText.text = MyData.Instance.stageInfo.curChapter + "";
		stageText.text = MyData.Instance.stageInfo.curStage + "";

		yield return new WaitForSecondsRealtime(0.4f);

		animator.SetTrigger("Start");
		SoundManager.Instance.PlaySFX("GameReady");
		SoundManager.Instance.PlayBGM("Stage1BGM");
		
		yield return new WaitForSecondsRealtime(6.3f);

		readyScreen.transform.DOMoveY(1.0f, 3f).OnComplete(() => { readyScreen.SetActive(false);});   //Delay CallBack
		StateChange(GameState.Play);
	}
	private void StageInit()
	{
		bgImg.sprite = bgSpr[MyData.Instance.stageInfo.curChapter - 1];

		switch (MyData.Instance.stageInfo.curChapter)
		{
			case 1:
				switch (MyData.Instance.stageInfo.curStage)
				{
					case 1:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, 1.71f, 2.08f);
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, -2.0f, 2.17f);
						break;
					case 2:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -1.96f, 2.08f);
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, -0.12f, 2.65f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, 1.99f, 2.28f);
						break;
					case 3:
						MonsterManager.Instance.MonsterCreate(MonsterType.Boss1, -0.12f, 2.65f);
						break;
					case 4:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -1.94f, 2.23f);
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -0.14f, 2.88f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, 1.65f, 2.32f);
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, 1.94f, -1.11f);
						break;
					case 5:
						MonsterManager.Instance.MonsterCreate(MonsterType.Boss2, 1.53f, 2.45f);
						break;
				}
				break;
			case 2:
				switch (MyData.Instance.stageInfo.curStage)
				{
					case 1:
						MonsterManager.Instance.MonsterCreate(MonsterType.Boss2, 1.71f, 2.08f);
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, -2.0f, 2.17f);
						break;
					case 2:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -1.96f, 2.08f);
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, -0.12f, 2.65f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, 1.99f, 2.28f);
						break;
					case 3:
						MonsterManager.Instance.MonsterCreate(MonsterType.Boss1, -0.12f, 2.65f);
						break;
					case 4:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -1.94f, 2.23f);
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -0.14f, 2.88f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, 1.65f, 2.32f);
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, 1.94f, -1.11f);
						break;
					case 5:
						MonsterManager.Instance.MonsterCreate(MonsterType.Boss2, 1.53f, 2.45f);
						break;
				}
				break;
			case 3:
				switch (MyData.Instance.stageInfo.curStage)
				{
					case 1:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, 1.71f, 2.08f);
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, -2.0f, 2.17f);
						break;
					case 2:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -1.96f, 2.08f);
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, -0.12f, 2.65f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, 1.99f, 2.28f);
						break;
					case 3:
						MonsterManager.Instance.MonsterCreate(MonsterType.Boss1, -0.12f, 2.65f);
						break;
					case 4:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -1.94f, 2.23f);
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -0.14f, 2.88f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, 1.65f, 2.32f);
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, 1.94f, -1.11f);
						break;
					case 5:
						MonsterManager.Instance.MonsterCreate(MonsterType.Boss2, 1.53f, 2.45f);
						break;
				}
				break;
		}

		curMonster = MonsterCheck();
	}
	public void BulletAllDestory()
	{
		GameObject[] Bullet = GameObject.FindGameObjectsWithTag("Bullet");

		for(int i = 0; i < Bullet.GetLength(0); i++)
		{
			Destroy(Bullet[i]);
		}
	}
	public void MonsterAllDestory()
	{
		GameObject[] monster = GameObject.FindGameObjectsWithTag("Monster");

		for(int i = 0; i < monster.GetLength(0); i++)
		{
			Destroy(monster[i]);
		}
	}
	IEnumerator Clear()
    {
		Time.timeScale = 0;
		SoundManager.Instance.PlaySFX("StageClearSFX");

		BulletAllDestory();
		MonsterAllDestory();

		TimeLimit.Instance.StopRegular();

		Time.timeScale = 1;
		clearPanel.SetActive(true);
		yield return new WaitForSecondsRealtime(3f);
		clearPanel.SetActive(false);


		if(MyData.Instance.stageInfo.curStage == 5)
		{
			clearChapterPanel.SetActive(true);
			GameResult.Instance.ScoreResult(TimeLimit.Instance.GetClearTime(), PlayerHp.Instance.GetHp(), Score.Instance.GetScore());
		}
		else
		{
			clearScreen.SetActive(true);
			StageClear.Instance.ScoreResult(TimeLimit.Instance.GetClearTime(), PlayerHp.Instance.GetHp(), Score.Instance.GetScore());
		}

		MyData.Instance.stageInfo.curStage++;
	}
	IEnumerator GameOver()
    {
		TimeLimit.Instance.StopRegular();
		Time.timeScale = 0;

		SoundManager.Instance.PlaySFX("GameReady");

		BulletAllDestory();
		MonsterAllDestory();

		failedPanel.SetActive(true);
		yield return new WaitForSecondsRealtime(3f);
		Time.timeScale = 1;

		failedPanel.SetActive(false);
		failedScreen.SetActive(true);
	}
	IEnumerator LoadSceneLobby()
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(4f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	IEnumerator LoadNextScene()
    {
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(4f);
	
		Scene scene = SceneManager.GetActiveScene();
		int curScene = scene.buildIndex;
		int nextScene = curScene + 1;
		SceneManager.LoadScene("Stage" + nextScene);
	}
	public int MonsterCheck()
	{
		GameObject[] monster = GameObject.FindGameObjectsWithTag("Monster");
		Debug.Log("감지된 몬스터 수 : " + monster.GetLength(0));
		return monster.GetLength(0);
	}
	public void MonsterDestory()
	{
		curMonster--;
		Debug.Log("삭제 된 후 수 : " + curMonster);

		if (curMonster <= 0)
		{
			StateChange(GameState.Clear);
		}
	}
}

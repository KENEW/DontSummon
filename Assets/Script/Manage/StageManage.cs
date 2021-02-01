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
				curMonster = MonsterCheck();
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
			//사이즈
			//빨 0.048(소), 0.056(중)
			//초 0.048(소) 0.067(중)
			//파 0.067(대)
			//빨간 보스 0.52(소) 0.6(중) 0.7(대)
			//초록 보스 0.04(소) 0.05(중) 0.055(대)
			//파랑 보스 0.25(대)
			case 1:
				switch (MyData.Instance.stageInfo.curStage)
				{ 
					case 1: //프리팹 사이즈가 중사이즈
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, 1.61f, 2.12f,0.067f,5,80);
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, -1.9f, 2.23f,0.048f,2,100);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 0f);
						break;
					case 2:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -1.91f, 1.73f,0.048f,2,100);
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, 1.91f, 1.82f,0.056f,5,80);
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, -0.06f, 2.73f,0.067f,10,50,2f);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 0.55f);
						break;
					case 3:
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, -0.06f, 2.54f,0.60f,20,80,2f);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
					case 4:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -1.78f, 2.32f,0.067f,5,80);
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, 0f, 2.73f, 0.056f,5,80);
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, 1.6f, 2.49f, 0.067f,10,50,2f);
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, 1.65f, -1f,0.067f,5,80);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
					case 5:
						MonsterManager.Instance.MonsterCreate(MonsterType.BossGreen, 1.69f, 2.5f,0.05f,20,80,1f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, -1.95f, -1.04f, 0.52f, 10, 100,2f);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
				}
				break;
			case 2:
				switch (MyData.Instance.stageInfo.curStage)
				{
					case 1:
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, -1.9f, 2.3f, 0.6f, 5, 100);
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, 1.8f, 2.3f, 0.048f, 2, 100);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
					case 2:
						MonsterManager.Instance.MonsterCreate(MonsterType.GreenMonster, -1.9f, 2.14f, 0.048f, 2, 100);
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, -0.14f, 2.75f, 0.067f, 10, 50,2f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossGreen, 1.78f, 2.31f, 0.05f, 5, 100);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
					case 3:
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, -1.88f, 2.2f, 0.6f, 10, 100,2f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, -0.01f, 2.59f, 0.6f, 10, 100,2f);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
					case 4:
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, 0f, 2.92f, 0.048f, 2, 100);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossGreen, 1.76f, 2.21f, 0.055f, 10, 100,2f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossBlue, -1.63f, -1.22f, 0.25f, 10, 100,2f);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
					case 5:
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, 1.9f, -1f, 0.52f, 10, 100,2f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossGreen, -0.06f, 2.77f, 0.055f, 15, 80,1f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossBlue, -1.65f, -1f, 0.25f, 15, 80,1f);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
				}
				break;
			case 3:
				switch (MyData.Instance.stageInfo.curStage)
				{
					case 1:
						MonsterManager.Instance.MonsterCreate(MonsterType.RedMonster, -1.81f, 2.47f, 0.056f, 5, 100);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossBlue, 0, 2.89f, 0.215f, 5, 120);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
					case 2:
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, -2f, 2.27f, 0.52f, 2, 100);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossGreen, 1.84f, 2.27f, 0.05f, 5, 120);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossBlue, -0.1f, 2.74f, 0.25f, 10, 80, 2f);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
					case 3:
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, -1.88f, 2.35f, 0.60f, 10, 100, 2f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossBlue, -0.01f, 2.9f, 0.215f, 10, 100, 2f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossGreen, 1.94f, -1f, 0.04f, 4, 100, 2f);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
					case 4:
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, -0.12f, 2.91f, 0.058f, 5, 100);
						MonsterManager.Instance.MonsterCreate(MonsterType.BlueMonster, 1.67f, -1.05f, 0.058f, 5, 100);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, -1.95f, 2.43f, 0.6f, 5, 120);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossGreen, 1.78f, 2.49f, 0.055f, 10, 100, 2f);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
					case 5:
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, -1.92f, -1.15f, 0.52f, 4, 120, 2f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossGreen, 1.95f, -1.05f, 0.04f, 4, 120, 2f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossGreen, 1.76f, 2.46f, 0.05f, 20, 100, 1.5f);
						MonsterManager.Instance.MonsterCreate(MonsterType.BossRed, -1.79f, 2.37f, 0.7f, 15, 80, 1f);
						GameObject.Find("Tiles").transform.position = new Vector2(0f, 1.3f);
						break;
				}
				break;
		}
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

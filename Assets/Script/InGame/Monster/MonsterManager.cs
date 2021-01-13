using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{ 
	RedMonster,
	GreenMonster,
	BlueMonster,
	BossRed,
	BossGreen,
	BossBlue
}

public class MonsterManager : MonoSingleton<MonsterManager>
{
	public GameObject[] monsterPre;
	public Transform parentTrans;

	public void MonsterCreate(MonsterType monsterType, float x, float y)
	{
		Vector2 t_pos = new Vector2(x, y);

		Instantiate(monsterPre[(int)monsterType], t_pos, Quaternion.identity, parentTrans);
	}

	public void MonsterCreate(MonsterType monsterType, float x, float y, float size)
	{
		Vector2 t_pos = new Vector2(x, y);

		GameObject t_obj = Instantiate(monsterPre[(int)monsterType], t_pos, Quaternion.identity, parentTrans);
		t_obj.transform.localScale = new Vector2(size, size);
	}

	public void MonsterCreate(MonsterType monsterType, float x, float y, float size, int hp, int score)
	{
		Vector2 t_pos = new Vector2(x, y);

		GameObject t_obj = Instantiate(monsterPre[(int)monsterType], t_pos, Quaternion.identity, parentTrans);
		t_obj.transform.localScale = new Vector2(size, size);

		t_obj.GetComponent<Monster>().maxHp = hp;
		t_obj.GetComponent<Monster>().acuireScore = score;
	}

	public void MonsterCreate(MonsterType monsterType, float x, float y, float size, int hp, int score, float spawnTime)
	{
		Vector2 t_pos = new Vector2(x, y);

		GameObject t_obj = Instantiate(monsterPre[(int)monsterType], t_pos, Quaternion.identity, parentTrans);
		t_obj.transform.localScale = new Vector2(size, size);

		t_obj.GetComponent<Monster>().maxHp = hp;
		t_obj.GetComponent<Monster>().acuireScore = score;
		t_obj.GetComponent<Monster>().bulletSpawnTime = spawnTime;
	}
}

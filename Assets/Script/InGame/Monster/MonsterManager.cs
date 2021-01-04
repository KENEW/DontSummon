using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{ 
	RedMonster,
	GreenMonster,
	BlueMonster,
	Boss1,
	Boss2,
	Boss3
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
}

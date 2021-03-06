﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum TypeColor
{
	Red = 0,
	Green = 1,
	Blue = 2
}

public enum MonsterFaceType
{
	Idle = 0,
	Hit,
	Die
}
public class Monster : MonoBehaviour
{
	public GameObject[] randomBulletPre;
	public GameObject bulletPre;

	public ParticleSystem hitEffect;
	public ParticleSystem dieEffect;

	protected PolygonCollider2D coll;
	protected SpriteRenderer sprRender;

	public Sprite[] monsterFace;

	protected int curHp;

	public int maxHp;
	public int acuireScore;

	public TypeColor typeColor;


	private Coroutine bulletSpawnCo;
	private Coroutine spawnRandomBulletCo;

	private Vector2 bulletPos;

	public Transform hpbarParent;

	public GameObject hpBarPre;

	public Image hpBarImg;

	private Vector2 pivotHpPos;

	public Animator animator;
	public SpriteOutline spriteOutline;

	public float bulletSpawnTime=4.0f;

	protected virtual void Start()
	{
		curHp = maxHp;

		bulletPos = new Vector2(transform.position.x, transform.position.y - gameObject.GetComponent<PolygonCollider2D>().bounds.extents.y - 0.45f);

		coll = GetComponent<PolygonCollider2D>();
		sprRender = GetComponent<SpriteRenderer>();
	
		hpBarImg = transform.Find("MonsterCanvas/Hp").GetComponent<Image>();

		bulletSpawnCo = StartCoroutine(BulletSpawn());
		spawnRandomBulletCo = StartCoroutine(SpawnRandomBullet());
	}
	IEnumerator DestoryDelay()
	{
		coll.enabled = false;
		sprRender.sprite = monsterFace[2];

		StopCoroutine(bulletSpawnCo);
		StopCoroutine(spawnRandomBulletCo);

		gameObject.transform.Find("MonsterCanvas/Hp").gameObject.SetActive(false);
		gameObject.transform.Find("MonsterCanvas/HpBackground").gameObject.SetActive(false);

		animator.SetTrigger("DieTrigger");
		Instantiate(dieEffect, transform.position, Quaternion.identity, gameObject.transform.parent);

		yield return new WaitForSeconds(1.0f);
		Destroy(gameObject, 0.1f);
	}
	protected IEnumerator MonsterChangeFace(Sprite changeSprite)
	{
		sprRender.sprite = changeSprite;
		yield return new WaitForSeconds(1f);
		sprRender.sprite = monsterFace[(int)MonsterFaceType.Idle];
	}
	protected IEnumerator BulletSpawn()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			//if(StageManage.Instance.GetGameState() == GameState.Play)
			{
				GameObject t_bullet = Instantiate(bulletPre, bulletPos, Quaternion.identity);
				t_bullet.GetComponent<Bullet>().SetBulletType(BulletType.Normal);
			}

			yield return new WaitForSeconds(bulletSpawnTime);
		}
	}
	protected IEnumerator SpawnRandomBullet() //특수 투사체 생성
	{
		while (true)
		{
			int randTime = (int)Random.Range(10, 20);
			yield return new WaitForSeconds(randTime);

			int t_type = (int)Random.Range(1, 5);
			//if (StageManage.Instance.GetGameState() == GameState.Play)
			{
				GameObject t_bullet = Instantiate(randomBulletPre[t_type - 1], bulletPos, Quaternion.identity);
				t_bullet.GetComponent<Bullet>().SetBulletType((BulletType)t_type);
			}
		}
	}
	public void GetDamage(int hpValue)
	{
		if (curHp - hpValue <= 0)
		{
			SoundManager.Instance.PlaySFX("MonsterDeathSFX");
			Score.Instance.AddScore(acuireScore * hpValue);
			curHp = 0;
			hpBarImg.fillAmount = 0;
			if(spriteOutline != null)
			{
				spriteOutline.outlineSize = 0;
			}

			StageManage.Instance.MonsterDestory();

			StartCoroutine(DestoryDelay());
		}
		else
		{
			SoundManager.Instance.PlaySFX("MonsterHitSFX");

			Score.Instance.AddScore(acuireScore * hpValue); 
			transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - 30), 0.25f)
			.OnComplete(() => { transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), 0.2f); });
			curHp -= hpValue;
			Instantiate(hitEffect, transform.position, transform.rotation);
			StartCoroutine(MonsterChangeFace(monsterFace[(int)MonsterFaceType.Hit]));
			hpBarImg.fillAmount -= (float)hpValue / maxHp;
		}
	}
	public void RecoveryHp(int hpValue) //회복
	{
		if (curHp + hpValue >= maxHp)
		{
			curHp = maxHp;
		}
		else
		{
			curHp += hpValue;
		}

		hpBarImg.fillAmount += (float)hpValue / maxHp;
	}
	public TypeColor GetTypeColor()
	{
		return typeColor;
	}
}

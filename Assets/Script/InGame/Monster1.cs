﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster1 : MonoBehaviour
{
    public ParticleSystem hitEffect;

    public Image hp;

    public GameObject bullet;
    public GameObject[] randomBullet;

    public Sprite[] monsterFace;
    private SpriteRenderer monsterFaceRenderer;
    private SpriteRenderer monsterRenderer;

    //private int monsterHp;
    public int monsterHp;
    [SerializeField]
    private int monsterMaxHp;

    public int monsterScore; //몬스터 피격 점수

    public float fadeTime = 0.9f;
    private bool isPlaying = false;

    public int waitingTime = 4;

    Vector2 bulletPos;

    void Start()
    {
        monsterRenderer = gameObject.GetComponent<SpriteRenderer>();
        monsterFaceRenderer = transform.Find("MonsterCanvas/Face").gameObject.GetComponent<SpriteRenderer>();

        bulletPos = new Vector2(transform.position.x, transform.position.y - gameObject.GetComponent<PolygonCollider2D>().bounds.extents.y - 0.45f);
        monsterHp = monsterMaxHp;
       
        StartCoroutine(BulletSpawn());
        StartCoroutine(SpawnRandomBullet());

    }
    public void Destroyed()
    {
        StopCoroutine(BulletSpawn());
        StopCoroutine(SpawnRandomBullet());

        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());

        monsterFaceRenderer.sprite = monsterFace[2];
        transform.Find("MonsterCanvas/Hp").gameObject.SetActive(false);
        transform.Find("MonsterCanvas/HpBackground").gameObject.SetActive(false);

        transform.Find("Explosion").gameObject.SetActive(true);
        transform.Find("MonsterCanvas/Face").gameObject.GetComponent<Animator>().SetTrigger("DieTrigger");
        gameObject.GetComponent<Animator>().SetTrigger("DieTrigger");
        //Destroy(gameObject);
    }
    IEnumerator MonsterChangeFace(Sprite changeSprite)
    {
        monsterFaceRenderer.sprite = changeSprite;
        yield return new WaitForSeconds(1f);
        monsterFaceRenderer.sprite = monsterFace[0];
    }
    IEnumerator BulletSpawn()
    {
        while(monsterHp>0)
        {
            yield return new WaitForSeconds(0.1f);
            Instantiate(bullet, bulletPos, Quaternion.identity);
            yield return new WaitForSeconds(waitingTime);
        }
    }
    IEnumerator SpawnRandomBullet() //특수 투사체 생성
    {
        while(monsterHp>0)
        {
            int randTime = (int)Random.Range(15, 20);
            yield return new WaitForSeconds(randTime);
            Instantiate(randomBullet[Random.Range(0, 4)], bulletPos, Quaternion.identity);
        }
    }
    public void GetDamage(int hpValue)
    {
        if (monsterHp - hpValue <= 0) //몬스터 죽음
        {
            if(monsterHp-hpValue<0)
            {
                Score.Instance.AddScore(monsterScore * (monsterHp+hpValue)); //몬스터 피격 시 점수 획득
            }
            else
            {
                Score.Instance.AddScore(monsterScore * hpValue); //몬스터 피격 시 점수 획득
            }

            monsterHp = 0;
            hp.fillAmount =0;
            SoundManager.Instance.PlaySFX("MonsterDeathSFX");
            
            Destroyed();
        }

        else
        {
            Score.Instance.AddScore(monsterScore * hpValue); //몬스터 피격 시 점수 획득
            monsterHp -= hpValue;
            SoundManager.Instance.PlaySFX("MonsterHitSFX");
            Instantiate(hitEffect,transform.position, transform.rotation); //hit effect
            StartCoroutine(MonsterChangeFace(monsterFace[1])); //우는 표정
            hp.fillAmount -= (float)hpValue / monsterMaxHp;
        }
       
    }
    public void RecoveryHp(int hpValue) //회복
    {
        if (monsterHp + hpValue >= monsterMaxHp)
        {
            monsterHp = monsterMaxHp;
        }
        else
        {
            monsterHp += hpValue;
        }

        hp.fillAmount += (float)hpValue / monsterMaxHp;
    }
    private void OnCollisionEnter2D(Collision2D coll) 
    {
        if (transform.CompareTag("RedMonster"))
        {
            if(coll.gameObject.tag=="RedBullet")
            {
                GetDamage(1);
                Debug.Log(monsterHp);
            }
        }
        else if (transform.CompareTag("GreenMonster"))
        {
            if (coll.gameObject.tag == "GreenBullet")
            {
                GetDamage(1);
                Debug.Log(monsterHp);
            }
        }
        else if (transform.CompareTag("BlueMonster"))
        {
            if (coll.gameObject.tag == "BlueBullet")
            {
                GetDamage(1);
                Debug.Log(monsterHp);
            }
        }  
    }

    public void GamePlayFalse() //게임 오버, 클리어 시
    {
        StopCoroutine(BulletSpawn());
        StopCoroutine(SpawnRandomBullet());
    }
}

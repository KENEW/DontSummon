using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster1 : MonoBehaviour
{
    public Image hp;

    public GameObject bullet;
    public GameObject[] randomBullet;

    public Sprite[] monsterFace;
    private SpriteRenderer monsterRenderer;

    //private int monsterHp;
    public int monsterHp;
    [SerializeField]
    private int monsterMaxHp;

    public int monsterScore; //몬스터 피격 점수

    float timer = 0f;
    public int waitingTime = 4;
    int bulletFlag = 1;

    Vector2 bulletPos;

    Score score;

 
    // Start is called before the first frame update
    void Start()
    {
        monsterHp = monsterMaxHp;

        bulletPos = new Vector2(transform.position.x, transform.position.y - 1.0f);
        StartCoroutine("BulletSpawn");

        InvokeRepeating("SpawnRandomBullet", 17, 17);

        score = GameObject.Find("ScorePanel").GetComponent<Score>();

        monsterRenderer = transform.Find("MonsterCanvas/Face").gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(monsterHp==0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator MonsterChangeFace(Sprite changeSprite)
    {
        monsterRenderer.sprite = changeSprite;
        yield return new WaitForSeconds(1f);
        monsterRenderer.sprite = monsterFace[0];
    }

    IEnumerator BulletSpawn()
    {
        while(true)
        {
            if(monsterHp>0)
            {
                yield return new WaitForSeconds(0.1f);
                Instantiate(bullet, bulletPos, Quaternion.identity);
                yield return new WaitForSeconds(waitingTime);
            }
        }
    }

    void SpawnRandomBullet() //특수 투사체 생성
    {
        Instantiate(randomBullet[Random.Range(0, 4)], bulletPos, Quaternion.identity);
    }

    public void GetDamage(int hpValue)
    {
        if (monsterHp - hpValue <= 0)
        {
            monsterHp = 0;
        }
        else
        {
            monsterHp -= hpValue;
        }

        hp.fillAmount -= (float)hpValue / monsterMaxHp;

        score.AddScore(monsterScore*hpValue); //몬스터 피격 시 점수 획득

        StartCoroutine(MonsterChangeFace(monsterFace[1])); //우는 표정
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

    

}

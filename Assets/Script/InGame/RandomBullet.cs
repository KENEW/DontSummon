using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBullet : MonoBehaviour
{
    PlayerHp playerHp;
    Monster1 monsterHp;

    public Rigidbody2D rigid;

    public GameObject[] bulletArr;
    public GameObject[] redBulletArr;
    public GameObject[] greenBulletArr;
    public GameObject[] blueBulletArr;

    private float moveSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        playerHp = GameObject.Find("HpPanel").GetComponent<PlayerHp>();

        //포탈을 향해 이동
        rigid.velocity = new Vector2(-rigid.transform.position.x * moveSpeed, -(rigid.transform.position.y + 4.27f) * moveSpeed);
    }
    
    void Update()
    {
        bulletArr = GameObject.FindGameObjectsWithTag("Bullet");
        redBulletArr = GameObject.FindGameObjectsWithTag("RedBullet");
        greenBulletArr = GameObject.FindGameObjectsWithTag("GreenBullet");
        blueBulletArr = GameObject.FindGameObjectsWithTag("BlueBullet");
    }

    void FixedUpdate()
    {
        if (rigid.velocity.x > 2f)
        {
            rigid.velocity = new Vector2(2f, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -2f)
        {
            rigid.velocity = new Vector2(-2f, rigid.velocity.y);
        }

        if (rigid.velocity.y > 2f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 2f);
        }
        else if (rigid.velocity.y < -2f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        SoundManager.Instance.PlaySFX("Tick");

        if (coll.gameObject.tag == "Portal")
        {
            if(transform.CompareTag("Heal"))
            {
                playerHp.RecoveryHp(1); //플레이어 hp 1 회복
                Destroy(gameObject);
                Debug.Log("플레이어 Heal " + playerHp.curHp);
            }
            else if(transform.CompareTag("Fire"))
            {
                playerHp.GetDamage(2); //플레이어 hp 2 감소
                Destroy(gameObject);
                Debug.Log("플레이어 Fire "+playerHp.curHp);
            }
            else if(transform.CompareTag("Bomb"))
            {
                //모든 투사체 삭제
                for(int i=0;i<bulletArr.Length;i++)
                {
                    Destroy(bulletArr[i]);
                }
                for (int i = 0; i < redBulletArr.Length; i++)
                {
                    Destroy(redBulletArr[i]);
                }
                for (int i = 0; i < greenBulletArr.Length; i++)
                {
                    Destroy(greenBulletArr[i]);
                }
                for (int i = 0; i < blueBulletArr.Length; i++)
                {
                    Destroy(blueBulletArr[i]);
                }

                Destroy(gameObject);
                Debug.Log("플레이어 Bomb");
            }
            else if(transform.CompareTag("Skull"))
            {
                if(!StageManage.Instance.playerGuard)
                {
                    StageManage.Instance.StageFailed(); //게임 오버
                }
               
                Destroy(gameObject);
                Debug.Log("플레이어 Skull");
            }
        }

        else if (coll.gameObject.tag == "RedMonster" || coll.gameObject.tag == "GreenMonster" || coll.gameObject.tag == "BlueMonster")
        {
            monsterHp = coll.gameObject.GetComponent<Monster1>();

            if (transform.CompareTag("Heal"))
            {
                monsterHp.RecoveryHp(1); //몬스터 hp 1 회복
                Destroy(gameObject);
                Debug.Log("몬스터 Heal " + monsterHp.monsterHp);
            }
            else if (transform.CompareTag("Fire"))
            {
                monsterHp.GetDamage(2); //몬스터 hp 2 감소
                Destroy(gameObject);
                Debug.Log("몬스터 Fire " +monsterHp.monsterHp);
            }
            else if (transform.CompareTag("Bomb"))
            {
                Destroy(gameObject);
                Debug.Log("Bomb");
            }
            else if (transform.CompareTag("Skull"))
            {
                monsterHp.GetDamage(2); //몬스터 hp 2 감소
                Destroy(gameObject);
                Debug.Log("몬스터 Skull " + monsterHp.monsterHp);
            }
        }

       

    }
}

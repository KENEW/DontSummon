using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster1 : MonoBehaviour
{
    public Image hp;

    public GameObject bullet;
    public GameObject[] randomBullet;
    public GameObject[] bulletArr;
    public GameObject[] redBulletArr;
    public GameObject[] greenBulletArr;
    public GameObject[] blueBulletArr;

    
    //private int monsterHp;
    public int monsterHp;
    [SerializeField]
    private int monsterMaxHp;

    float timer = 0f;
    int waitingTime = 5;
    int bulletFlag = 1;

    Vector2 bulletPos;

 
    // Start is called before the first frame update
    void Start()
    {
        monsterHp = monsterMaxHp;

        bulletPos = new Vector2(transform.position.x, transform.position.y - 1.0f);
        Instantiate(bullet, bulletPos, Quaternion.identity);

        InvokeRepeating("SpawnRandomBullet", 17, 17);
    }

    // Update is called once per frame
    void Update()
    {
        /*bulletArr = GameObject.FindGameObjectsWithTag("Bullet");
        redBulletArr = GameObject.FindGameObjectsWithTag("RedBullet");
        greenBulletArr = GameObject.FindGameObjectsWithTag("GreenBullet");
        blueBulletArr = GameObject.FindGameObjectsWithTag("BlueBullet");

        int bulletNum = bulletArr.Length + redBulletArr.Length + greenBulletArr.Length + blueBulletArr.Length;*/ //투사체 개수 

        timer += Time.deltaTime;

        /*if(bulletFlag<monsterMaxHp) //몬스터의 hp만큼 생성
        {
            if (timer > waitingTime)
            {
                Instantiate(bullet, bulletPos, Quaternion.identity);
                bulletFlag += 1;
                timer = 0;
                
            }
        }

        else if (bulletNum<monsterHp && monsterHp != 0)
        {
            if (timer > waitingTime)
            {
                Instantiate(bullet, bulletPos, Quaternion.identity);
                bulletFlag += 1;
                timer = 0;

            }
        } */

        if(monsterHp>0)
        {
            if (timer > waitingTime)
            {
                Instantiate(bullet, bulletPos, Quaternion.identity);
                //bulletFlag += 1;
                timer = 0;
            }
        }

        else if(monsterHp==0)
        {
            Destroy(gameObject);
        }

        
        //DrawHp();

    }

    /*private void DrawHp()
    {
        for (int i = 0; i < monsterHp; i++)
        {
            hpObject[i].SetActive(true);
            //animator.SetTrigger("HpAnime");
        }
        for (int i = monsterHp; i < monsterMaxHp; i++)
        {
            hpObject[i].SetActive(false);
            //animator.SetFloat("Reverse", -1);
            //animator.SetTrigger("HpAnime");

        }
    }*/

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

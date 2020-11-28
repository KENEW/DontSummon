using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster1 : MonoBehaviour
{
    public Image hp;

    public GameObject bullet;
    public GameObject[] bulletArr;
    public GameObject[] redBulletArr;
    public GameObject[] greenBulletArr;
    public GameObject[] blueBulletArr;

    
    private int monsterHp;
    [SerializeField]
    private int monsterMaxHp;

    float timer = 0f;
    int waitingTime = 3;
    int bulletFlag = 1;

    Vector2 bulletPos;

 
    // Start is called before the first frame update
    void Start()
    {
        monsterHp = monsterMaxHp;

        bulletPos = new Vector2(transform.position.x, transform.position.y - 1.0f);
        Instantiate(bullet, bulletPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        bulletArr = GameObject.FindGameObjectsWithTag("Bullet");
        redBulletArr = GameObject.FindGameObjectsWithTag("RedBullet");
        greenBulletArr = GameObject.FindGameObjectsWithTag("GreenBullet");
        blueBulletArr = GameObject.FindGameObjectsWithTag("BlueBullet");

        int bulletNum = bulletArr.Length + redBulletArr.Length + greenBulletArr.Length + blueBulletArr.Length; //투사체 개수

        timer += Time.deltaTime;

        if(bulletFlag<monsterMaxHp) //몬스터의 hp만큼 생성
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
        }

        if(monsterHp==0)
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

    private void Damage()
    {
        hp.fillAmount -= 1.0f / monsterMaxHp;
    }

    private void OnCollisionEnter2D(Collision2D coll) //현재
    {

        if (transform.CompareTag("RedMonster"))
        {
            if(coll.gameObject.tag=="RedBullet")
            {
                Damage();
                monsterHp -= 1;
                Debug.Log(monsterHp);
            }
        }

        else if (transform.CompareTag("GreenMonster"))
        {
            if (coll.gameObject.tag == "GreenBullet")
            {
                Damage();
                monsterHp -= 1;
                Debug.Log(monsterHp);
            }
        }

        else if (transform.CompareTag("BlueMonster"))
        {
            if (coll.gameObject.tag == "BlueBullet")
            {
                Damage();
                monsterHp -= 1;
                Debug.Log(monsterHp);
            }
        }  

    }
    

}

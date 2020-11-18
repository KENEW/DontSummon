using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : MonoBehaviour
{
    public GameObject[] hpObject;

    public GameObject bullet;
    SpriteRenderer renderer;

    [SerializeField]
    private int monsterHp;
    [SerializeField]
    private int monsterMaxHp;

    float timer = 0f;
    int waitingTime = 5;

    Vector2 bulletPos;

   

    // Start is called before the first frame update
    void Start()
    {

        bulletPos = new Vector2(transform.position.x, transform.position.y - 1.0f);
        Instantiate(bullet, bulletPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if(monsterHp>0) //몬스터의 hp가 남아있으면 5초 간격으로 생성
        {
            if (timer > waitingTime)
            {
                Instantiate(bullet, bulletPos, Quaternion.identity);
                timer = 0;
            }
        }

        else if(monsterHp==0)
        {
            Destroy(gameObject);
        }

        DrawHp();
        
    }

    private void DrawHp()
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
    }


    private void OnCollisionEnter2D(Collision2D coll) //현재
    {
        renderer = GetComponent<SpriteRenderer>();

        if (transform.CompareTag("RedMonster"))
        {
            if(coll.gameObject.tag=="RedBullet")
            {
                monsterHp -= 1;
                Debug.Log(monsterHp);
            }
        }

        else if (transform.CompareTag("GreenMonster"))
        {
            if (coll.gameObject.tag == "GreenBullet")
            {
                monsterHp -= 1;
                Debug.Log(monsterHp);
            }
        }

        else if (transform.CompareTag("BlueMonster"))
        {
            if (coll.gameObject.tag == "BlueBullet")
            {
                monsterHp -= 1;
                Debug.Log(monsterHp);
            }
        }  

    }
    

}

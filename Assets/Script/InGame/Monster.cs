using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Rigidbody2D rigid;
    private float moveSpeed;
    //QuestDirector questDirector;
    NeedMonster needMonster;


    void Start()
    {
        //questDirector = GameObject.Find("QuestDirector").GetComponent<QuestDirector>();
        needMonster = GameObject.Find("NeedMonster").GetComponent<NeedMonster>();

        if (transform.CompareTag("Small"))
        {
            moveSpeed = 0.8f;
        }
        else if(transform.CompareTag("Medium"))
        {
            moveSpeed = 0.6f;
        }
        else
        {
            moveSpeed = 0.4f;
        }

        rigid.velocity = new Vector2(-rigid.transform.position.x * moveSpeed, -rigid.transform.position.y * moveSpeed);
    }

    void FixedUpdate()
    {
        if (transform.CompareTag("Small"))
        {
            moveSpeed = 0.4f;
        }
        else if (transform.CompareTag("Medium"))
        {
            moveSpeed = 0.3f;
        }
        else
        {
            moveSpeed = 0.2f;
        }



    }

    void OnMouseDown()
    {
        rigid.velocity = new Vector2(rigid.transform.position.x*moveSpeed, rigid.transform.position.y*moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
         //퀘스트
        if(transform.CompareTag("Small"))
        {
            if (needMonster.needNum != 0 && needMonster.monsterFlag==0) 
            {
                needMonster.Success();
            }
            else
            {
                needMonster.Fail();
            }
        }

        if (transform.CompareTag("Medium"))
        {
            if (needMonster.needNum != 0 && needMonster.monsterFlag == 1)
            { 
                needMonster.Success();
            }
            else
            {
                needMonster.Fail();
            }
        }

        if (transform.CompareTag("Large"))
        {
            if (needMonster.needNum != 0 && needMonster.monsterFlag == 2)
            {
                needMonster.Success();
            }
            else
            {
                needMonster.Fail();
            }
        }
       

        Debug.Log("소환");
        Destroy(gameObject);
    }
}

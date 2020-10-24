using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Rigidbody2D rigid;

    [SerializeField]
    private Vector2 curDir = new Vector2(1f, 0.5f);

    private float moveSpeed;
    private float power = 1.5f;

    //QuestDirector questDirector;
    NeedMonster needMonster;

    Vector2 tempVec;


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

        rigid.velocity = new Vector2(-rigid.transform.position.x * moveSpeed * 0.2f, -rigid.transform.position.y * moveSpeed * 0.2f);
    }

    /*void FixedUpdate()
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



    }*/

    /*void OnMouseDown()
    {
        rigid.velocity = new Vector2(rigid.transform.position.x*moveSpeed, rigid.transform.position.y*moveSpeed);
    }*/

    public void SetPower(float powerValue)
    {
        power = powerValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.Instance.PlaySFX("Tick");
        tempVec = rigid.velocity;
        //Debug.Log("현재 벨로시티 : " + tempVec);
        tempVec.Normalize();
        //Debug.Log("벨로시티 정규화 : " + tempVec);
        rigid.velocity = tempVec * power;
        //Debug.Log("변환된 벨로시티 : " + rigid.velocity);
    }

    private void OnTriggerEnter2D(Collider2D coll) //퀘스트
    {
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

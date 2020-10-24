using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Rigidbody2D rigid;
    private float moveSpeed;
    public QuestDirector questDirector;

    

    void Start()
    {
        questDirector = GameObject.Find("QuestDirector").GetComponent<QuestDirector>();

        if (transform.CompareTag("Small"))
        {
            moveSpeed = 0.4f;
        }
        else if(transform.CompareTag("Medium"))
        {
            moveSpeed = 0.3f;
        }
        else
        {
            moveSpeed = 0.2f;
        }

        rigid.velocity = new Vector2(-rigid.transform.position.x * moveSpeed, -rigid.transform.position.y * moveSpeed);
    }

    void OnMouseDown()
    {
        rigid.velocity = new Vector2(rigid.transform.position.x * moveSpeed, rigid.transform.position.y * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(transform.CompareTag("Small"))
        {
            if (questDirector.num[0] != 0)
            {
                questDirector.Success(0);
            }
            else
            {
                questDirector.Fail();
            }
        }

        if (transform.CompareTag("Medium"))
        {
            if (questDirector.num[1] != 0)
            { 
                questDirector.Success(1);
            }
            else
            {
                questDirector.Fail();
            }
        }

        if (transform.CompareTag("Large"))
        {
            if (questDirector.num[2] != 0)
            {
                questDirector.Success(2);
            }
            else
            {
                questDirector.Fail();
            }
        }

        Debug.Log("소환");
        Destroy(gameObject);
    }
}

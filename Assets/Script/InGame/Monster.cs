using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Rigidbody2D rigid;
    private float moveSpeed;

    void Start()
    {
        if(transform.CompareTag("Small"))
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

    void OnMouseDown()
    {
        rigid.velocity = new Vector2(rigid.transform.position.x * moveSpeed, rigid.transform.position.y * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("소환");
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medium : MonoBehaviour
{
    public Rigidbody2D rigid;
    private float moveSpeed = 0.7f;

    void Start()
    {
        rigid.velocity = new Vector2(-rigid.transform.position.x * moveSpeed, -rigid.transform.position.y * moveSpeed);
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {

        Debug.Log("소환");
        Destroy(gameObject);
    }

}

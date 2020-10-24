using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : MonoBehaviour
{
    public Rigidbody2D rigid;

    [SerializeField]
    private Vector2 curDir = new Vector2(1f, 0.5f);

    private float moveSpeed = 0.1f;

    private Vector2 portal = new Vector2(0.0f, 0.0f);


    void Start()
    {
        rigid.velocity = new Vector2(-rigid.transform.position.x, -rigid.transform.position.y);
    }

    Vector2 tempVelo;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Ball"))
        //{
        //    rigid.velocity = Vector3.zero;
        //}
    }

}

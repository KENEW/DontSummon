using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Large : MonoBehaviour
{
    public Rigidbody2D rigid;

    [SerializeField]
    private Vector2 curDir = new Vector2(1f, 0.5f);

    private float moveSpeed = 0.04f;

    private Vector2 portal = new Vector2(0.0f, 0.0f);

    void Update()
    {
        Vector2 position = Vector2.MoveTowards(transform.position, portal, moveSpeed);
        rigid.MovePosition(position);
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

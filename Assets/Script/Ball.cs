using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigid;

    [SerializeField]
    private Vector2 curDir = new Vector2(1f, 0.5f);

    private float moveSpeed = 200f;

    void Start()
    {
        //rigid.AddForce(curDir * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        rigid.velocity = new Vector2(4, 2);
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

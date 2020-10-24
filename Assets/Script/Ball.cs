using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigid;

    [SerializeField]
    private Vector2 curDir = new Vector2(1f, 0.5f);
    Vector2 tempVec;
    private float moveSpeed = 200f;

    private float power = 1.5f;

    void Start()
    {
        rigid.velocity = new Vector2(4, 2);
    }

    public void SetPower(float powerValue)
    {
        power = powerValue;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        SoundManager.Instance.PlaySFX("Hit");
        tempVec = rigid.velocity;
        //Debug.Log("현재 벨로시티 : " + tempVec);
        tempVec.Normalize();
        //Debug.Log("벨로시티 정규화 : " + tempVec);
        rigid.velocity = tempVec * power;
        //Debug.Log("변환된 벨로시티 : " + rigid.velocity);
    }

}

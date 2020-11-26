using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigid;
    private SpriteRenderer renderer;
    public Sprite[] sprites;

    [SerializeField]
    private Vector2 curDir = new Vector2(1f, 0.5f);

    private float moveSpeed=0.2f;
    private float power;

    PlayerHp playerHp;


    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        playerHp = GameObject.Find("HpPanel").GetComponent<PlayerHp>();

        //포탈을 향해 이동
        rigid.velocity = new Vector2(-rigid.transform.position.x * moveSpeed, -(rigid.transform.position.y+4.27f) * moveSpeed);

    }

   
    public void SetPower(float powerValue)
    {
        power = powerValue;
    }


    private void OnCollisionEnter2D(Collision2D coll) //포탈이나 몬스터에 닿으면 destroy
    {
        SoundManager.Instance.PlaySFX("Tick");

        if (coll.gameObject.tag == "Portal")
        {
            playerHp.GetDamage(1);
            Destroy(gameObject);
        }

        else if (coll.gameObject.tag == "RedMonster")
        {
            Destroy(gameObject);
        }

        else if (coll.gameObject.tag == "GreenMonster")
        {
            Destroy(gameObject);
        }

        else if (coll.gameObject.tag == "BlueMonster")
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D coll) //타일
    {
        if(coll.CompareTag("RedTile"))
        {
            //renderer.color = new Color(1.0f, 0f, 0f);
            renderer.sprite = sprites[0];
            gameObject.tag = "RedBullet";
        }

        else if (coll.CompareTag("GreenTile"))
        {
            //renderer.color = new Color(0f, 1.0f, 0f);
            renderer.sprite = sprites[1];
            gameObject.tag = "GreenBullet";
        }

        else if (coll.CompareTag("BlueTile"))
        {
            //renderer.color = new Color(0f, 0f, 1.0f);
            renderer.sprite = sprites[2];
            gameObject.tag = "BlueBullet";
        }

       
    }
}

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

    private float moveSpeed=0.3f;
    private float power;

    PlayerHp playerHp;
    Player player;

    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        playerHp = GameObject.Find("HpPanel").GetComponent<PlayerHp>();
        player = GameObject.Find("TouchObject").GetComponent<Player>();


        //포탈을 향해 이동
        rigid.velocity = new Vector2(-rigid.transform.position.x * moveSpeed, -(rigid.transform.position.y+4.27f) * moveSpeed);
    }
   void FixedUpdate()
    {
        if(rigid.velocity.x>2f)
        {
            rigid.velocity = new Vector2(2f, rigid.velocity.y);
        }
        else if(rigid.velocity.x<-2f)
        {
            rigid.velocity = new Vector2(-2f, rigid.velocity.y);
        }

        if(rigid.velocity.y>2f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 2f);
        }
        else if(rigid.velocity.y<-2f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -2f);
        }
    }
    public void SetPower(float powerValue)
    {
        power = powerValue;
    }
    private void OnCollisionEnter2D(Collision2D coll) //포탈이나 몬스터에 닿으면 destroy
    {
        if (coll.gameObject.tag == "Portal")
        {
            player.ChangeFace(1); //angry face

            playerHp.GetDamage(1);
            Destroy(gameObject);
        }

        else if (coll.gameObject.tag == "RedMonster")
        {
            if(transform.CompareTag("RedBullet"))
            {
                player.ChangeFace(2); //happy face
            }
            Destroy(gameObject);
        }

        else if (coll.gameObject.tag == "GreenMonster")
        {
            if (transform.CompareTag("GreenBullet"))
            {
                player.ChangeFace(2); //happy face
            }
            Destroy(gameObject);
        }

        else if (coll.gameObject.tag == "BlueMonster")
        {
            if (transform.CompareTag("BlueBullet"))
            {
                player.ChangeFace(2); //happy face
            }
            Destroy(gameObject);
        }
        else
        {
            SoundManager.Instance.PlaySFX("Tick");
        }
    }
    private void OnTriggerEnter2D(Collider2D coll) //타일
    {
        if(coll.CompareTag("RedTile"))
        {
            coll.GetComponent<Animator>().SetTrigger("BulletTrigger");
            //renderer.color = new Color(1.0f, 0f, 0f);
            renderer.sprite = sprites[0];
            gameObject.tag = "RedBullet";
        }

        else if (coll.CompareTag("GreenTile"))
        {
            coll.GetComponent<Animator>().SetTrigger("BulletTrigger");
            //renderer.color = new Color(0f, 1.0f, 0f);
            renderer.sprite = sprites[1];
            gameObject.tag = "GreenBullet";
        }

        else if (coll.CompareTag("BlueTile"))
        {
            coll.GetComponent<Animator>().SetTrigger("BulletTrigger");
            //renderer.color = new Color(0f, 0f, 1.0f);
            renderer.sprite = sprites[2];
            gameObject.tag = "BlueBullet";
        }    
    }
}

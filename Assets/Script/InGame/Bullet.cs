using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Normal = 0,
    Heal,
    Fire,
    Bomb,
    Skull
}

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer sprRender;
    public Sprite[] sprites;

    [SerializeField]
    private Vector2 curDir = new Vector2(1f, 0.5f);

    private float moveSpeed=0.3f;
    private float power;

    public TypeColor bulletColor;
    public BulletType bulletType;

    void Start()
    {
        sprRender = gameObject.GetComponent<SpriteRenderer>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        //포탈을 향해 이동
        rigid.velocity = new Vector2(-rigid.transform.position.x * moveSpeed, -(rigid.transform.position.y+4.27f) * moveSpeed);
    }
	void FixedUpdate()
	{
		if (rigid.velocity.x > 2f)
		{
			rigid.velocity = new Vector2(2f, rigid.velocity.y);
		}
		else if (rigid.velocity.x < -2f)
		{
			rigid.velocity = new Vector2(-2f, rigid.velocity.y);
		}

		if (rigid.velocity.y > 2f)
		{
			rigid.velocity = new Vector2(rigid.velocity.x, 2f);
		}
		else if (rigid.velocity.y < -2f)
		{
			rigid.velocity = new Vector2(rigid.velocity.x, -2f);
		}
	}
    public void SetBulletType(BulletType bulletType)
    {
        this.bulletType = bulletType;
    }
    public BulletType GetBulletType()
    {
        return bulletType;
	}
    private void OnCollisionEnter2D(Collision2D coll) //포탈이나 몬스터에 닿으면 destroy
    {
        if (coll.gameObject.tag == "Portal")
        {
            switch(bulletType)
            {
                case BulletType.Heal :
                    PlayerHp.Instance.RecoveryHp(1);
                    Player.Instance.ChangeFace((int)PlayerFaceState.Happy);
                    break;
                case BulletType.Fire:
                    PlayerHp.Instance.GetDamage(2);
                    Player.Instance.ChangeFace((int)PlayerFaceState.Angry);
                    break;
                case BulletType.Skull:
                    PlayerHp.Instance.GetDamage(3);
                    Player.Instance.ChangeFace((int)PlayerFaceState.Angry);
                    break;
                case BulletType.Bomb:
                    //PlayerHp.Instance.GetDamage(3);
                    GameObject[] Bullet = GameObject.FindGameObjectsWithTag("Bullet");

                    for (int i = 0; i < Bullet.GetLength(0); i++)
                    {
                        Destroy(Bullet[i]);
                    }
                    Player.Instance.ChangeFace((int)PlayerFaceState.Angry);
                    break;
                case BulletType.Normal:
                    PlayerHp.Instance.GetDamage(1);
                    Player.Instance.ChangeFace((int)PlayerFaceState.Angry);
                    break;
            }
            
            Destroy(gameObject);
        }
        else if (coll.gameObject.tag == "Monster")
        {
            switch (bulletType)
            {
                case BulletType.Heal:
                    coll.transform.GetComponent<Monster>().RecoveryHp(1);
                    break;
                case BulletType.Fire:
                    coll.transform.GetComponent<Monster>().GetDamage(2);
                    break;
                case BulletType.Skull:
                    coll.transform.GetComponent<Monster>().GetDamage(2);
                    break;
                case BulletType.Bomb:
                    coll.transform.GetComponent<Monster>().GetDamage(1);
                    break;
                case BulletType.Normal:

                    TypeColor t_collColor = coll.transform.GetComponent<Monster>().GetTypeColor();

                    if (t_collColor == bulletColor)
                    {
                        Player.Instance.ChangeFace((int)PlayerFaceState.Happy);
                        coll.transform.GetComponent<Monster>().GetDamage(1);
                    }

                    break;
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
        if(bulletType == BulletType.Normal)
        {
            if (coll.CompareTag("RedTile"))
            {
                coll.GetComponent<Animator>().SetTrigger("BulletTrigger");
                sprRender.sprite = sprites[(int)TypeColor.Red];
                bulletColor = TypeColor.Red;
            }
            else if (coll.CompareTag("GreenTile"))
            {
                coll.GetComponent<Animator>().SetTrigger("BulletTrigger");
                sprRender.sprite = sprites[(int)TypeColor.Green];
                bulletColor = TypeColor.Green;
            }
            else if (coll.CompareTag("BlueTile"))
            {
                coll.GetComponent<Animator>().SetTrigger("BulletTrigger");
                sprRender.sprite = sprites[(int)TypeColor.Blue];
                bulletColor = TypeColor.Blue;
            }
        }
         
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWall : MonoBehaviour
{
    public int wallHp;
    Color color;
    SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        wallHp = 3;

        spr = GetComponent<SpriteRenderer>();
        color = spr.color;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (wallHp==2)
        {
            color.a = 0.66f;
            spr.color = color;
        }

        else if (wallHp == 1)
        {
            color.a = 0.33f;
            spr.color = color;
        }

        else if(wallHp ==0)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D coll) //공이 닿았을 때
    {
        if (coll.gameObject.tag == "RedBullet")
        {
            wallHp -= 1;
        }

        else if (coll.gameObject.tag == "GreenBullet")
        {
            wallHp -= 1;
        }

        else if (coll.gameObject.tag == "BlueBullet")
        {
            wallHp -= 1;
        }
        
        else if (coll.gameObject.tag == "Bullet")
        {
            wallHp -= 1; 
        }

        else if (coll.gameObject.tag == "Heal")
        {
            wallHp -= 1;
        }

        else if (coll.gameObject.tag == "Fire")
        {
            wallHp -= 1;
        }

        else if (coll.gameObject.tag == "Bomb")
        {
            wallHp -= 1;
        }

        else if (coll.gameObject.tag == "Skull")
        {
            wallHp -= 1;
        }

    }
}

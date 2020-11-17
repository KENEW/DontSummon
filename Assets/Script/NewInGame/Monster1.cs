using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : MonoBehaviour
{
    public GameObject bullet;
    private SpriteRenderer renderer;

    int monsterHp = 5;

    float timer = 0f;
    int waitingTime = 5;

    Vector2 bulletPos;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();

        bulletPos = new Vector2(transform.position.x, transform.position.y - 1.0f);
        Instantiate(bullet, bulletPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(monsterHp>0) //몬스터의 hp가 남아있으면 5초 간격으로 생성
        {
            if (timer > waitingTime)
            {
                Instantiate(bullet, bulletPos, Quaternion.identity);
                timer = 0;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D coll) 
    {
        /*
        if(transform.CompareTag("RedMonster"))
        {
            if(coll.renderer.color ==  Color(1.0f,0f,0f))
            {
                monsterHp -= 1;
                Debug.Log(monsterHp);
            }
        }

        else if (transform.CompareTag("GreenMonster"))
        {
            if (coll.renderer.color == new Color(0f, 1.0f, 0f))
            {
                monsterHp -= 1;
                Debug.Log(monsterHp);
            }
        }

        else if (transform.CompareTag("BlueMonster"))
        {
            if (coll.renderer.color == new Color(0f, 0f, 1.0f))
            {
                monsterHp -= 1;
                Debug.Log(monsterHp);
            }
        }
        */
        

    }

}

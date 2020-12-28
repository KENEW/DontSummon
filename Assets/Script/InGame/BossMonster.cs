using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    private SpriteRenderer renderer;
    private SpriteRenderer monsterRenderer;
    public Sprite[] sprites;
    public Sprite[] bossFace;


    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        monsterRenderer = transform.Find("MonsterCanvas/Face").gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Boss());
    }

    IEnumerator Boss()
    {
        while(true)
        {
            renderer.sprite = sprites[0];
            monsterRenderer.sprite = bossFace[0];
            gameObject.tag = "RedMonster";
            yield return new WaitForSeconds(3f);

            renderer.sprite = sprites[1];
            monsterRenderer.sprite = bossFace[2];
            gameObject.tag = "GreenMonster";
            yield return new WaitForSeconds(3f);

            renderer.sprite = sprites[2];
            monsterRenderer.sprite = bossFace[4];
            gameObject.tag = "BlueMonster";
            yield return new WaitForSeconds(3f);
        }
        
    }

    IEnumerator BossMonsterChangeFace(Sprite changeSprite)
    {
        monsterRenderer.sprite = changeSprite;
        yield return new WaitForSeconds(1f);
        if(transform.CompareTag("RedMonster"))
        {
            monsterRenderer.sprite = bossFace[0];
        }
        else if (transform.CompareTag("GreenMonster"))
        {
            monsterRenderer.sprite = bossFace[2];
        }
        else if (transform.CompareTag("BlueMonster"))
        {
            monsterRenderer.sprite = bossFace[4];
        }
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (transform.CompareTag("RedMonster"))
        {
            if (coll.gameObject.tag == "RedBullet")
            {
                StartCoroutine(BossMonsterChangeFace(bossFace[1])); //빨강 움
            }
        }
        else if (transform.CompareTag("GreenMonster"))
        {
            if (coll.gameObject.tag == "GreenBullet")
            {
                StartCoroutine(BossMonsterChangeFace(bossFace[3])); //초록 움
            }
        }
        else if (transform.CompareTag("BlueMonster"))
        {
            if (coll.gameObject.tag == "BlueBullet")
            {
                StartCoroutine(BossMonsterChangeFace(bossFace[5])); //파랑 움
            }
        }
    }
}

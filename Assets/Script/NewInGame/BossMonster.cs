using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    private SpriteRenderer renderer;
    public Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Boss());

    }

    IEnumerator Boss()
    {
        while(true)
        {
            renderer.sprite = sprites[0];
            gameObject.tag = "RedMonster";
            yield return new WaitForSeconds(3f);
            renderer.sprite = sprites[1];
            gameObject.tag = "GreenMonster";
            yield return new WaitForSeconds(3f);
            renderer.sprite = sprites[2];
            gameObject.tag = "BlueMonster";
            yield return new WaitForSeconds(3f);
        }
        
    }
}

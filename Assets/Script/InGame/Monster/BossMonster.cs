using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    public bool autoColor = false;

    protected override void Start()
    {
        base.Start();

        spriteOutline = GetComponent<SpriteOutline>();

        if(autoColor)
        {
            StartCoroutine(Boss());
        }
    }
    IEnumerator Boss()
    {
        float delay = 3f;

        while(true)
        {
            typeColor = TypeColor.Red;
            spriteOutline.color = Color.red;
            yield return new WaitForSeconds(delay);

            typeColor = TypeColor.Green;
            spriteOutline.color = Color.green;
            yield return new WaitForSeconds(delay);

            typeColor = TypeColor.Blue;
            spriteOutline.color = Color.blue;
            yield return new WaitForSeconds(delay);
        }
    }
}

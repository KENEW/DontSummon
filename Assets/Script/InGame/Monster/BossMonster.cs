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
            if(typeColor == TypeColor.Red)
            {
                StartCoroutine(RedStartBoss());
            }

            if (typeColor == TypeColor.Green)
            {
                StartCoroutine(GreenStartBoss());
            }

            if (typeColor == TypeColor.Blue)
            {
                StartCoroutine(BlueStartBoss());
            }
        }
    }

    IEnumerator RedStartBoss()
    {
        float delay = 3f;

        while (true)
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

    IEnumerator GreenStartBoss()
    {
        float delay = 3f;

        while (true)
        {
            typeColor = TypeColor.Green;
            spriteOutline.color = Color.green;
            yield return new WaitForSeconds(delay);

            typeColor = TypeColor.Blue;
            spriteOutline.color = Color.blue;
            yield return new WaitForSeconds(delay);

            typeColor = TypeColor.Red;
            spriteOutline.color = Color.red;
            yield return new WaitForSeconds(delay);
        }

    }

    IEnumerator BlueStartBoss()
    {
        float delay = 3f;

        while (true)
        {
            typeColor = TypeColor.Blue;
            spriteOutline.color = Color.blue;
            yield return new WaitForSeconds(delay);

            typeColor = TypeColor.Red;
            spriteOutline.color = Color.red;
            yield return new WaitForSeconds(delay);

            typeColor = TypeColor.Green;
            spriteOutline.color = Color.green;
            yield return new WaitForSeconds(delay);
        }

    }
}

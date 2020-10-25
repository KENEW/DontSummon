using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoSingleton<MonsterGenerator>
{
    public GameObject smallPre;
    public GameObject mediumPre;
    public GameObject largePre;

    private int smallNum = 3;
    private int mediumNum = 2;
    private int largeNum = 1;

    public int i = 0;

	private void Start()
	{
        MosnterCreate();

    }
	public void MosnterCreate()
    {
        for (int i = 0; i < smallNum; i++) //소
        {
            Instantiate(smallPre, RandomPosition(5.6f, 7.4f), Quaternion.identity);

        }

        for (int i = 0; i < mediumNum; i++) //중
        {
            Instantiate(mediumPre, RandomPosition(5.6f, 7.3f), Quaternion.identity);
        }

        for (int i = 0; i < largeNum; i++) //대
        {
            Instantiate(largePre, RandomPosition(5.6f, 7.3f), Quaternion.identity);
        }
    }

    public Vector2 RandomPosition(float hei, float wid) //랜덤 위치
    {
        int signX = 1;
        int signY = 1;

        //Vector2 randomPos = Random.insideUnitCircle * radius;
        Vector2 randomPos = new Vector2(Random.Range(-wid / 2, wid / 2),Random.Range(-hei/2, hei/2));

        if(randomPos.x<1.8f && randomPos.y<1.8f) //안쪽 사각형 내부
        {
            if(randomPos.x > -1.8f && randomPos.y > -1.8f)
            {
                if(randomPos.x<0)
                {
                    signX = -1;
                }
                if(randomPos.y<0)
                {
                    signY = -1;
                }
                randomPos.x += 0.7f * signX;
                randomPos.y += 0.7f * signY;
            }
            
        }

        return randomPos;
    }  
}
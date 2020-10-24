using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public GameObject smallPre;
    public GameObject mediumPre;
    public GameObject largePre;
    public GameObject[] monsterObject;

    private float radius = 5.0f; //원안의 랜덤 위치
    private int smallNum = 3;
    private int mediumNum = 2;
    private int largeNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i=0; i<smallNum;i++) //소
        {
            Instantiate(smallPre, RandomPosition(radius), Quaternion.identity);
        }

        for(int i=0;i<mediumNum;i++) //중
        {
            Instantiate(mediumPre, RandomPosition(radius), Quaternion.identity);
        }

        for(int i=0;i<largeNum;i++) //대
        {
            Instantiate(largePre, RandomPosition(radius), Quaternion.identity);
        }

      

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Vector2 RandomPosition(float radius) //랜덤 위치
    {
        //Vector2 getPoint = Random.insideUnitCircle;
        //float r = Random.Range(0.0f, radius);
        //return (getPoint * r) + transform.position;
        int signX = 1;
        int signY = 1;

        Vector2 randomPos = Random.insideUnitCircle * radius;

        if(randomPos.x<2.5f && randomPos.y<2.5f) //원반 위에 있게
        {
            if(randomPos.x > -2.5f && randomPos.y > -2.5f)
            {
                if(randomPos.x<0)
                {
                    signX = -1;
                }
                if(randomPos.y<0)
                {
                    signY = -1;
                }
                randomPos.x += 2.5f * signX;
                randomPos.y += 2.5f * signY;
            }
            
        }

        return randomPos;
    }
}

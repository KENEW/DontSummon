using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    public int stage;
    public int hp;
    public int score;
    public int curTime;
}

public class MyData : SceneSingleTon<MyData>
{
    public DefenceWall curDefenceWall = DefenceWall.rentangle;

    public StageInfo stageInfo = new StageInfo();

    public int[] stageScore = new int[3];
    public void SaveScore()
    {
        
	}
    public void InitState()
    {
        stageInfo.score = 0;
        stageInfo.hp = 3;
        stageInfo.curTime = 100;
        curDefenceWall = DefenceWall.rentangle;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyData : SceneSingleTon<MyData>
{
    public DefenceWall curDefenceWall = DefenceWall.rentangle;

    public string loginID = string.Empty;

    public int curStage = 0;
    public int curHp = 3;
    public int curScore = 0;
    public float curTIme = 100;

    public int[] stageScore = new int[3];

    public void InitState()
    {
        curStage = 0;
        curHp = 3;
        curTIme = 100;
        curDefenceWall = DefenceWall.rentangle;
    }
}

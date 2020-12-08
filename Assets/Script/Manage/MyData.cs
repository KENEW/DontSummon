using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyData : SceneSingleTon<MyData>
{
    public static int curStage = 0;
    public static DefenceWall curDefenceWall = DefenceWall.rentangle;

    public void SetDefenceWall(DefenceWall defenceWall)
    {
        curDefenceWall = defenceWall;
    }
    public void SetStage(int stage)
    {
        curStage = stage;
	}
}

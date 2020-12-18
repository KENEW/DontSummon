using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageInfo
{
    public int curStage = 0;
    public int curHp = 3;
    public int curScore = 0;
    public float curTIme = 60;

    public DefenceWall curDefenceWall = DefenceWall.rentangle;
}

public class MyData : SceneSingleTon<MyData>
{
    public string loginID = string.Empty;
    public int[] stageScore = new int[3];

    public StageInfo stageInfo = new StageInfo();

    public void InitState()
    {
        stageInfo.curStage = 0;
        stageInfo.curHp = 3;
        stageInfo.curTIme = 60;
        stageInfo.curDefenceWall = DefenceWall.rentangle;
    }
    public void SetStageScore(int stage, int score)
    {
        stageScore[stage] = score > stageScore[stage] ? score : stageScore[stage];

        BackEndFederationAuth.Instance.OnSetLeaderBoard(stage, score);

	}
}

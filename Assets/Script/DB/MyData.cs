using System.Collections;
using System.Collections.Generic;

using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

using UnityEngine;
using UnityEngine.UI;

using AESWithJava.Con;
using LitJson;

public class StageInfo
{
    public int curStage;
    public int curChapter;

    public int curHp;
    public int curScore;
    public float curTIme;

    public DefenceWall curDefenceWall;

    public StageInfo()
    {
        curStage = 1;
        curChapter = 1;

        curHp = 3;
        curTIme = 60;
        curScore = 0;

        curDefenceWall = DefenceWall.rentangle;
    }
}

[System.Serializable]
public class ScoreInfo
{
    public int[] stageScore = new int[3] { 0, 0, 0 };
}

public class MyData : SceneSingleTon<MyData>
{
    public StageInfo stageInfo = new StageInfo();
    public ScoreInfo scoreInfo;

    public int curScore = 0;
    public string loginID = String.Empty;

    [SerializeField]
    private string filePath = "/GameData/";

    private void Update()
	{
        //Test Key
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGameData();
		}
        if (Input.GetKeyDown(KeyCode.O))
        {
            DataEdit();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            DataInit();
        }
    }
    public void DataInit()
    {
        curScore = 0;
        stageInfo = new StageInfo();
	}
    public void DataEdit()  //Data test
    {
        scoreInfo.stageScore[0] = 9999;
        scoreInfo.stageScore[1] = 9999;
        scoreInfo.stageScore[2] = 9999;

        SaveGameData();
    }
    public void LoadGameData()
    {
        string filePath_gameData = Application.dataPath + filePath + "GameData.json";

        if (File.Exists(filePath_gameData))
        {
            Debug.Log("스코어 정보 불러오기 성공!");
            string FromJsonData = File.ReadAllText(filePath_gameData);
            FromJsonData = Program.Decrypt(FromJsonData, "userdata");
            scoreInfo = JsonUtility.FromJson<ScoreInfo>(FromJsonData);
        }
        else
        {
            Debug.Log("새로운 스코어 정보 파일 생성");
            scoreInfo = new ScoreInfo();
        }
    }
    public void SaveGameData()
    {
        JsonData jsonData_scoreData = JsonMapper.ToJson(scoreInfo);
        string scoreData = Program.Encrypt(jsonData_scoreData.ToString(), "userdata");

        File.WriteAllText(Application.dataPath + filePath + "GameData.json", scoreData);

        Debug.Log("저장 완료");
    }
}

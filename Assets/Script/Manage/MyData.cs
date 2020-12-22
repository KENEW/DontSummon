using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using UnityEngine.UI;

[System.Serializable]
public class StageInfo
{
    public int curStage = 1;
    public int curChapter = 1;

    public int curHp = 3;
    public int curScore = 0;
    public float curTIme = 60;

    public DefenceWall curDefenceWall = DefenceWall.rentangle;
}

public class MyData : SceneSingleTon<MyData>
{
    public string loginID = string.Empty;
    public int[] stageScore = new int[3] { 0, 0, 0 };

    public StageInfo stageInfo = new StageInfo();

    //test
    public void DataEdit()
    {
        stageScore[0] = 300;
        stageScore[1] = 500;
        stageScore[2] = 700;

        SaveData();
    }
    public void InitState()
    {
        stageInfo.curStage = 0;
        stageInfo.curHp = 3;
        stageInfo.curTIme = 60;
        stageInfo.curDefenceWall = DefenceWall.rentangle;
    }
    public void SetScore(int[] score)
    {
        stageScore = score;
	}
    public void SaveData()
    {
        for (int i = 0; i < 3; i++)
        {
            string _value = stageScore[i].ToString();
            SetString("stageScore" + (i + 1).ToString(), _value, GetUserKey("user"));
        }

        PlayerPrefs.Save();
    }
    public void LoadData()
    {
        for (int i = 0; i < 3; i++)
		{
			stageScore[i] = int.Parse(GetString("stageScore" + (i + 1).ToString(), GetUserKey("user")));
			Debug.Log("스코어 : " + i + " " + stageScore[i]);
        }
    }
    private byte[] GetUserKey(string userKey)
    {
        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userKey));
    
        return secret;
    }
    public void SetStageScore(int stage, int score)
    {
        stageScore[stage] = score > stageScore[stage] ? score : stageScore[stage];

        BackEndFederationAuth.Instance.OnSetLeaderBoard(stage, score);
	}
    public void SetString(string _key, string _value, byte[] _secret)
    {
        // Hide '_key' string.  
        MD5 md5Hash = MD5.Create();
        byte[] hashData = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
        string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

        // Encrypt '_value' into a byte array  
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_value);

        // Eecrypt '_value' with 3DES.  
        TripleDES des = new TripleDESCryptoServiceProvider();
        des.Key = _secret;
        des.Mode = CipherMode.ECB;
        ICryptoTransform xform = des.CreateEncryptor();
        byte[] encrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

        // Convert encrypted array into a readable string.  
        string encryptedString = Convert.ToBase64String(encrypted);

        // Set the ( key, encrypted value ) pair in regular PlayerPrefs.  
        PlayerPrefs.SetString(hashKey, encryptedString);

        Debug.Log("SetString hashKey: " + hashKey + " Encrypted Data: " + encryptedString);
    }
    public static string GetString(string _key, byte[] _secret)
    {
        // Hide '_key' string.  
        MD5 md5Hash = MD5.Create();
        byte[] hashData = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
        string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

        // Retrieve encrypted '_value' and Base64 decode it.  
        string _value = PlayerPrefs.GetString(hashKey);
        byte[] bytes = Convert.FromBase64String(_value);

        // Decrypt '_value' with 3DES.  
        TripleDES des = new TripleDESCryptoServiceProvider();
        des.Key = _secret;
        des.Mode = CipherMode.ECB;
        ICryptoTransform xform = des.CreateDecryptor();
        byte[] decrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

        // decrypte_value as a proper string.  
        string decryptedString = System.Text.Encoding.UTF8.GetString(decrypted);

        Debug.Log("GetString hashKey: " + hashKey + " GetData: " + _value + " Decrypted Data: " + decryptedString);

        return decryptedString;
    }
}

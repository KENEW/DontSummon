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

[System.Serializable]
public class ScoreInfo
{
    public int[] stageScore = new int[3] { 0, 0, 0 };
}

public class MyData : SceneSingleTon<MyData>
{
    public string loginID = string.Empty;

    public StageInfo stageInfo = new StageInfo();
    public bool isLoginCheck = false;

    public string filePath = "/GameData/";

    public ScoreInfo scoreInfo;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
        {
            SaveGameData();
		}
        if (Input.GetKeyDown(KeyCode.O))
        {
            DataEdit();
        }
    }
	public void LoadGameData()
    {
        string filePath_gameData = Application.dataPath + filePath + "GameData.json";

        if (File.Exists(filePath_gameData))
        {
            Debug.Log("스코어 정보 불러오기 성공!");
            string FromJsonData = File.ReadAllText(filePath_gameData);
            scoreInfo = JsonUtility.FromJson<ScoreInfo>(FromJsonData);
        }
        else
        {
            Debug.Log("새로운 파일 생성");
            scoreInfo = new ScoreInfo();
        }
    }
    public void SaveGameData()
    {
        JsonData jsonData_scoreData = JsonMapper.ToJson(scoreInfo);

        File.WriteAllText(Application.dataPath + filePath + "GameData.json", jsonData_scoreData.ToString());

        Debug.Log("저장 완료");
    }
    public void DataEdit()  //Data test
    {
        scoreInfo.stageScore[0] = 500;
        scoreInfo.stageScore[1] = 700;
        scoreInfo.stageScore[2] = 1000;

        SaveGameData();
    }
    public void InitState()
    {
        stageInfo.curStage = 1;
        stageInfo.curHp = 3;
        stageInfo.curTIme = 60;
        stageInfo.curScore = 0;
    }
}
//	public void SaveData()
//	{
//		for (int i = 0; i < 3; i++)
//		{
//			string _value = stageScore[i].ToString();
//			SetString("stageScore" + (i + 1).ToString(), _value, GetUserKey("user"));
//		}

//		PlayerPrefs.Save();
//	}
//	public void LoadData()
//	{
//		SaveData();
//		for (int i = 0; i < 3; i++)
//		{
//			stageScore[i] = int.Parse(GetString("stageScore" + (i + 1).ToString(), GetUserKey("user")));
//			Debug.Log("스코어 : " + i + " " + stageScore[i]);
//		}
//	}
//	private byte[] GetUserKey(string userKey)
//	{
//		MD5 md5Hash = new MD5CryptoServiceProvider();
//		byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userKey.Length.ToString()));

//		return secret;
//	}
//	public void SetStageScore(int stage, int score)
//	{
//		stageScore[stage] = score > stageScore[stage] ? score : stageScore[stage];

//		BackEndFederationAuth.Instance.OnSetLeaderBoard(stage, score);
//	}
//	public void SetString(string _key, string _value, byte[] _secret)
//	{
//		// Hide '_key' string.  
//		MD5 md5Hash = MD5.Create();
//		byte[] hashData = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
//		string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

//		// Encrypt '_value' into a byte array  
//		byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_value);

//		// Eecrypt '_value' with 3DES.  
//		TripleDES des = new TripleDESCryptoServiceProvider();
//		des.Key = _secret;
//		des.Mode = CipherMode.ECB;
//		ICryptoTransform xform = des.CreateEncryptor();
//		byte[] encrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

//		// Convert encrypted array into a readable string.  
//		string encryptedString = Convert.ToBase64String(encrypted);

//		// Set the ( key, encrypted value ) pair in regular PlayerPrefs.  
//		PlayerPrefs.SetString(hashKey, encryptedString);

//		Debug.Log("SetString hashKey: " + hashKey + " Encrypted Data: " + encryptedString);
//	}
//	public static string GetString(string _key, byte[] _secret)
//	{
//		// Hide '_key' string.  
//		MD5 md5Hash = MD5.Create();
//		byte[] hashData = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key.Length.ToString()));
//		string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

//		// Retrieve encrypted '_value' and Base64 decode it.  
//		string _value = PlayerPrefs.GetString(hashKey);
//		byte[] bytes = Convert.FromBase64String(_value);

//		// Decrypt '_value' with 3DES.  
//		TripleDES des = new TripleDESCryptoServiceProvider();
//		des.Key = _secret;
//		des.Mode = CipherMode.ECB;
//		ICryptoTransform xform = des.CreateDecryptor();
//		byte[] decrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

//		// decrypte_value as a proper string.  
//		string decryptedString = System.Text.Encoding.UTF8.GetString(decrypted);
//		Debug.Log("GetString hashKey: " + hashKey + " GetData: " + _value + " Decrypted Data: " + decryptedString);

//		return decryptedString;
//	}
//}

namespace AESWithJava.Con
{
    class Program
    {
        static void Main(string[] args)

        {

            String originalText = "plain text";

            String key = "key";

            String en = Encrypt(originalText, key);

            String de = Decrypt(en, key);



            Console.WriteLine("Original Text is " + originalText);

            Console.WriteLine("Encrypted Text is " + en);

            Console.WriteLine("Decrypted Text is " + de);

        }



        public static string Decrypt(string textToDecrypt, string key)

        {

            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            rijndaelCipher.Mode = CipherMode.CBC;

            rijndaelCipher.Padding = PaddingMode.PKCS7;



            rijndaelCipher.KeySize = 128;

            rijndaelCipher.BlockSize = 128;

            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);

            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);

            byte[] keyBytes = new byte[16];

            int len = pwdBytes.Length;

            if (len > keyBytes.Length)

            {

                len = keyBytes.Length;

            }

            Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;

            rijndaelCipher.IV = keyBytes;

            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(plainText);

        }



        public static string Encrypt(string textToEncrypt, string key)

        {

            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            rijndaelCipher.Mode = CipherMode.CBC;

            rijndaelCipher.Padding = PaddingMode.PKCS7;



            rijndaelCipher.KeySize = 128;

            rijndaelCipher.BlockSize = 128;

            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);

            byte[] keyBytes = new byte[16];

            int len = pwdBytes.Length;

            if (len > keyBytes.Length)

            {

                len = keyBytes.Length;

            }

            Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;

            rijndaelCipher.IV = keyBytes;

            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();

            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);

            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));

        }



    }

}

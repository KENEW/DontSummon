using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWallManage : MonoBehaviour
{
    public static DefenceWall curDefenseWall;
    public GameObject[] defenseWall; 

    // Start is called before the first frame update
    void Start()
    {
        //생성
        if (curDefenseWall == DefenceWall.rentangle) //직사각형
        {
            Instantiate(defenseWall[0], new Vector2(-1.74f,-3.89f), Quaternion.identity);
            Instantiate(defenseWall[0], new Vector2(1.74f, -3.89f), Quaternion.identity);
        }
        else if (curDefenseWall == DefenceWall.triangle) //삼각형
        {
            Instantiate(defenseWall[1], new Vector2(-1.74f, -3.89f), Quaternion.identity);
            Instantiate(defenseWall[2], new Vector2(1.74f, -3.89f), Quaternion.identity);
        }
        else if (curDefenseWall == DefenceWall.square) //정사각형
        {
            Instantiate(defenseWall[3], new Vector2(-2.36f, -3.94f), Quaternion.identity);
            Instantiate(defenseWall[3], new Vector2(-2.36f, -2.7f), Quaternion.identity);
            Instantiate(defenseWall[3], new Vector2(2.36f, -3.94f), Quaternion.identity);
            Instantiate(defenseWall[3], new Vector2(2.36f, -2.7f), Quaternion.identity);
        }

    }


}

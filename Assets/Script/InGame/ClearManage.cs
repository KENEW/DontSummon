using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearManage : MonoBehaviour
{
    GameObject[] red;
    GameObject[] green;
    GameObject[] blue;

    public int clearScore;
    public bool flag = true;
    private int allMonsterHp;

    bool redDie, greenDie, blueDie;

    TimeLimit time;
    Score score;

    void Start()
    {
        time = GameObject.Find("TimePanel").GetComponent<TimeLimit>();
        score = GameObject.Find("ScorePanel").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        red = GameObject.FindGameObjectsWithTag("RedMonster");
        green = GameObject.FindGameObjectsWithTag("GreenMonster");
        blue = GameObject.FindGameObjectsWithTag("BlueMonster");

        /*for(int i=0;i<red.Length;i++)
        {
            if (red[i].GetComponent<Monster1>().isDie == true)
            {
                redDie = true;
            }
            else
            {
                redDie = false;
                break;
            }
        }

        for (int i = 0; i < green.Length; i++)
        {
            if (green[i].GetComponent<Monster1>().isDie == true)
            {
                greenDie = true;
            }
            else
            {
                greenDie = false;
                break;
            }
        }

        for (int i = 0; i < blue.Length; i++)
        {
            if (blue[i].GetComponent<Monster1>().isDie == true)
            {
                blueDie = true;
            }
            else
            {
                blueDie = false;
                break;
            }
        }

        if (redDie == true && greenDie == true && blueDie == true && flag == true)
        {
            flag = false;
            StageManage.Instance.StageClear();
            time.ClearTime();
            score.AddScore(time.clearTime * clearScore);
        }*/

        if (red.Length == 0 && green.Length==0 && blue.Length==0  && flag == true)
        {
            flag = false;
            StageManage.Instance.OnStageClear();
            time.GetClearTime();
            score.AddScore(time.clearTime*clearScore);
        }
    }
}

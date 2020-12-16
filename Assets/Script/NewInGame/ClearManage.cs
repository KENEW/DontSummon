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

        if (red.Length == 0 && green.Length == 0 && blue.Length == 0 && flag == true)
        {
            flag = false;
            StageManage.Instance.StageClear();
            time.ClearTime();
            score.AddScore(time.clearTime*clearScore);
        }
    }
}

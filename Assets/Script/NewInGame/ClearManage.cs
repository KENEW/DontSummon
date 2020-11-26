using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearManage : MonoBehaviour
{
    GameObject[] red;
    GameObject[] green;
    GameObject[] blue;

    // Update is called once per frame
    void Update()
    {
        red = GameObject.FindGameObjectsWithTag("RedMonster");
        green = GameObject.FindGameObjectsWithTag("GreenMonster");
        blue = GameObject.FindGameObjectsWithTag("BlueMonster");

        if (red.Length == 0 && green.Length == 0 && blue.Length == 0)
        {
            StageManage.Instance.StageClear();
        }
    }
}

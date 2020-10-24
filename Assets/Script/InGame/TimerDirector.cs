using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDirector : MonoBehaviour
{
    public float limitTime;
    public Text timerText;


    // Start is called before the first frame update
    void Update()
    {
        limitTime -= Time.deltaTime;
        timerText.text = "Time : " + Mathf.Round(limitTime);

        if (Mathf.Round(limitTime) <= 0) //타임이 0이 되면
        {
            //GetComponent<Text>().color = new Color(0.66f, 0.62f, 0.26f);
            timerText.text = "Game Over";
        }
    }

}

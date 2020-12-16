using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn : MonoBehaviour
{
    bool isPause;

    // Update is called once per frame
    /*void Update()
    {
        if (this.isPause == true)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }*/

    public void Pause()
    {
        if(isPause==false)
        {
            this.isPause = true;
            Time.timeScale = 0;
        }
        else
        {
            this.isPause = false;
            Time.timeScale = 1;
        }
    }
}

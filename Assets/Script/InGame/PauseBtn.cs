using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBtn : MonoBehaviour
{
    public GameObject PauseScreen;

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
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
    }

    public void Stop()
    {
        SceneManager.LoadScene("Lobby");
    }
}

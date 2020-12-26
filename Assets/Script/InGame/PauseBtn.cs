using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBtn : MonoBehaviour
{
    public GameObject PauseScreen;

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

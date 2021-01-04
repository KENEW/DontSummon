using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBtn : MonoBehaviour
{
    public GameObject PauseScreen;

    public void Pause()
    {
        StageManage.Instance.StateChange(GameState.Pause);
        PauseScreen.SetActive(true);
    }

    public void Continue()
    {
        StageManage.Instance.StateChange(GameState.Play);
        PauseScreen.SetActive(false);
    }

    public void Stop()
    {
        SoundManager.Instance.PlaySFX("ButtonSFX");
        SceneManager.LoadScene("Lobby");
    }
}

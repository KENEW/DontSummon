using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StageFailed : MonoBehaviour
{
    public LightLoading lightLoading;

    public void FailedLobby()
    {
        SoundManager.Instance.PlaySFX("Button");
        lightLoading.LoadStart("Lobby");
    }
    public void FailedRestart()
    {
        SoundManager.Instance.PlaySFX("Button");
        MyData.Instance.InitState();
        lightLoading.LoadStart(SceneManager.GetActiveScene().name);
    }
}

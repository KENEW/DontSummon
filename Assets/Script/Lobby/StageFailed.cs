using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StageFailed : MonoBehaviour
{
    public LightLoading lightLoading;

    public void OnLobby()
    {
        Debug.Log("타임스케일"+Time.timeScale);
        SoundManager.Instance.PlaySFX("Button");
        lightLoading.LoadStart("Lobby");
    }

    public void OnReStart()
    {
        Debug.Log("타임스케일"+Time.timeScale);
        SoundManager.Instance.PlaySFX("Button");
        lightLoading.LoadStart("Stage" + (MyData.Instance.stageInfo.curStage).ToString());
    }

}

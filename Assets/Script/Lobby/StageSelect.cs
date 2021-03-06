﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageSelect : MonoBehaviour
{
    public GameObject scrollBar;

    public GameObject stageSelectPanel;
    public GameObject titlePanel;
    public GameObject defenceWallSelectPanel;

    public Button selectButton;

    public Text stageNumText;
    public Text highScoreText;
    public Text stageNameText;
    public Text storyText;

    [SerializeField]
    private float scrollPos = 0;
    [SerializeField]
    private float[] contentPos;

    public int curChapter = 0;

    public Vector2 selectScaleFalse = new Vector3(0.7f, 0.7f);
    public Vector2 selectScaleTrue = new Vector3(0.55f, 0.55f);

    public string[] stageStory = new string[3];
    public string[] stageName = new string[3];
    public int[] highScore = new int[3];

    private void Update()
    {
        contentPos = new float[transform.childCount];
        float distance = 1.0f / (contentPos.Length - 1.0f);

        for (int i = 0; i < contentPos.Length; i++)
        {
            contentPos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < contentPos.Length; i++)
            {
                if (scrollPos < (contentPos[i] + (distance / 2.0f)) && (scrollPos > contentPos[i] - (distance / 2.0f)))
                {
                    curChapter = i;
                    UIUpdate();
                    stageNumText.text = (curChapter + 1) + "";
                    scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, contentPos[i], 0.1f);
                }
            }
        }
        for (int i = 0; i < contentPos.Length; i++)
        {
            if (scrollPos < (contentPos[i] + (distance / 2.0f)) && (scrollPos > contentPos[i] - (distance / 2.0f)))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, selectScaleFalse, 0.1f);
                for (int a = 0; a < contentPos.Length; a++)
                {
                    transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, selectScaleTrue, 0.1f);
                }
            }
        }
    }
    public void StageSelectButton()
    {
        MyData.Instance.stageInfo.curChapter = curChapter + 1;
        SoundManager.Instance.PlaySFX("Button");

        stageSelectPanel.SetActive(false);
        defenceWallSelectPanel.SetActive(true);
    }
    public void BackButton()
    {
        SoundManager.Instance.PlaySFX("BackButton");

        stageSelectPanel.SetActive(false);
        titlePanel.SetActive(true);
    }
    private void UIUpdate()
    {
        if(curChapter == 0)
        {
            selectButton.interactable = true;
        }
        if(curChapter == 1)
        {
            selectButton.interactable = (MyData.Instance.scoreInfo.stageScore[0] > 0) ? true : false;
        }
        if (curChapter == 2)
        {
            selectButton.interactable = (MyData.Instance.scoreInfo.stageScore[1] > 0) ? true : false;
        }

        storyText.text = stageStory[curChapter];
        highScoreText.text = "최고 스코어 : " + MyData.Instance.scoreInfo.stageScore[curChapter];
        stageNameText.text = stageName[curChapter];
    }
}

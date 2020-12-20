using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum DefenceWall
{
    rentangle,
    triangle,
    square
}

public class DefenceWallSelect : MonoBehaviour
{
    public GameObject fadePanel;

    public GameObject scrollBar;
    public GameObject defenceWallSelectWindow;
    public GameObject stageSelectWindow;

    public LoadScene loadScene;

    public Text defenceWallNameText;
    public Text defenceWallHpText;
    public Text defenceWallInfoText;

    [SerializeField]
    private float scrollPos = 0;
    [SerializeField]
    private float[] contentPos;

    public DefenceWall curDefenceWall = DefenceWall.rentangle;

    public Vector2 selectScaleFalse = new Vector3(0.5f, 0.5f);
    public Vector2 selectScaleTrue = new Vector3(0.39f, 0.39f);

    public int[] defenceWallHp = new int[] {3, 1, 2};
    public string[] defenceWallNameStr;
    public string[] defenceWallInfoStr;

	private void Start()
	{
        loadScene = FindObjectOfType<LoadScene>();
    }
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
                    curDefenceWall = (DefenceWall)i;
                    UIUpdate();
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
    public void GameStart()
    {
        SoundManager.Instance.PlaySFX("Button");

        fadePanel.SetActive(true);
        fadePanel.GetComponent<Image>().DOFade(1.0f, 2.0f).OnComplete(() =>
        {
            MyData.Instance.stageInfo.curDefenceWall = curDefenceWall;
            loadScene.LoadStart("Stage1");
        });
    }
    public void BackButton()
    {
        SoundManager.Instance.PlaySFX("Button");
        defenceWallSelectWindow.SetActive(false);
        stageSelectWindow.SetActive(true);
    }
    private void UIUpdate()
    {
        defenceWallNameText.text = defenceWallNameStr[(int)curDefenceWall];
        defenceWallHpText.text = defenceWallHp[(int)curDefenceWall] + "";
        defenceWallInfoText.text = defenceWallInfoStr[(int)curDefenceWall] + "";
    }
}
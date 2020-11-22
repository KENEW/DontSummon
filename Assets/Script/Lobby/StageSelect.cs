using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageSelect : MonoBehaviour
{
    public GameObject scrollBar;

    public Text stageNum;

    [SerializeField]
    private float scrollPos = 0;
    [SerializeField]
    private float[] contentPos;

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
                    stageNum.text = (i + 1) + "";
                    scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, contentPos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < contentPos.Length; i++)
        {
            if (scrollPos < (contentPos[i] + (distance / 2.0f)) && (scrollPos > contentPos[i] - (distance / 2.0f)))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int a = 0; a < contentPos.Length; a++)
                {
                    transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                }
            }
        }
    }

    public void GameStart()
    {
		SoundManager.Instance.PlaySFX("Button");
        SceneManager.LoadScene("editScene");
    }
}

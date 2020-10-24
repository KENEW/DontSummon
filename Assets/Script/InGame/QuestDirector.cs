using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDirector : MonoBehaviour
{
    public int[] num = new int[3];

    public Text quest;

    public Image smallImage;
    public Image mediumImage;
    public Image largeImage;

    // Start is called before the first frame update
    void Start()
    {
        //smallImage.SetActive(false);
        //mediumImage.SetActive(false);
        //largeImage.SetActive(false);

        quest.text = "";

        for (int i = 0; i < num.Length; i++)
        {
            if (num[i] != 0)
            {
                quest.text += "×" + num[i];
            }
        }

        /*if(smallNum!=0)
        {
            //smallImage.SetActive(true);
            quest.text += " × " + smallNum;
        }
        if (mediumNum != 0)
        {
            //mediumImage.SetActive(true);
            quest.text += " × " + mediumNum;
        }
        if (largeNum != 0)
        {
           //largeImage.SetActive(true);
            quest.text += " × " + largeNum;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        quest.text = "×" + num[0] +"\n"+ "×" + num[1] + "\n"+"×" + num[2];
    }

    public void Success(int j)
    {
        num[j] -= 1;
    }

    public void Fail()
    {

    }
}

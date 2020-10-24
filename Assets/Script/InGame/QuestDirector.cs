using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDirector : MonoBehaviour
{
    public int[] needNum = new int[3]; //필요한 숫자
    public NeedMonster[] needMonster;

    //public Text quest;

    //public Image smallImage;
    //public Image mediumImage;
    //public Image largeImage;



    // Start is called before the first frame update
    void Start()
    {


        /*quest.text = "";

        for (int i = 0; i < needNum.Length; i++)
        {
            if (needNum[i] != 0)
            {
                quest.text += needNum[i];
            }
        }*/

        for (int i = 0; i < needNum.Length; i++)
        {
            if (needNum[i] != 0)
            {
                needMonster[i].SetNeedText(needNum[i]);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < needNum.Length; i++)
        {
            if (needNum[i] != 0)
            {
                needMonster[i].SetNeedText(needNum[i]);
            }
        }
        //quest.text = "×" + needNum[0] + "\n" + "×" + needNum[1] + "\n" + "×" + needNum[2];
    }

    public void Success(int j)
    {
        needNum[j] -= 1;
    }

    public void Fail()
    {
        //라이프가 깎임
    }

}


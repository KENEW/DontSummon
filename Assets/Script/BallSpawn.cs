using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    public GameObject ballPre;

    [SerializeField]
    private new Vector2 startPos = new Vector2(0, 0);

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(ballPre, startPos, Quaternion.identity);
		}
    }
}

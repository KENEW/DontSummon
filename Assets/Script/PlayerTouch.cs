using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{
    public GameObject touchObj;

    private float CapsuleSize = 0.3f;

    private bool monsterCheck = true;
    void Start()
    {
        touchObj.SetActive(false);
    }

	private void Update()
	{
        if(Input.GetKeyDown(KeyCode.R))
        {
            GameObject[] allBall = GameObject.FindGameObjectsWithTag("Monster");
            for(int i = 0; i< allBall.Length; i++)
            {
                Vector2 dir =  new Vector3(0f, 0f, 0f) - allBall[i].transform.position;
                dir.Normalize();
                allBall[i].GetComponent<Ball>().rigid.velocity = dir * 1.5f;

            }
		}
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pivotPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapCircle(pivotPos, CapsuleSize);

            if (collider != null)
            {
                if (collider.CompareTag("Monster"))
                {
                    monsterCheck = true;
                    Debug.Log("몬스터가 감지되었습니다.");
                }
            }
            else
            {
                monsterCheck = false;
            }
            
        }

        bool arrayCheck = false;
        int layerMask = (1 << LayerMask.NameToLayer("ArrayCheck"));

        if (Input.GetMouseButton(0))
        {
            if (!monsterCheck)
            {
                Vector2 pivotPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D collider = Physics2D.OverlapCircle(pivotPos, CapsuleSize, layerMask);

                if(collider.transform.gameObject == null)
                {
                    Debug.Log("아님");
                    touchObj.SetActive(false);
                }
                else if (collider.CompareTag("Array"))
                {
                    Debug.Log(collider.gameObject);
                    touchObj.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    touchObj.transform.position = new Vector3(touchObj.transform.position.x, touchObj.transform.position.y, 0);
                    touchObj.SetActive(true);
                }
            }    
        }
        if(Input.GetMouseButtonUp(0))
        {
            touchObj.SetActive(false);
		}
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(touchObj.transform.position, CapsuleSize);
    }
}

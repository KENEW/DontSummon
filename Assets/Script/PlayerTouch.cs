using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor;
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
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pivotPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapCircle(pivotPos, CapsuleSize);

            if (collider != null)
            {
                if (collider.CompareTag("Monster"))
                {
                    monsterCheck = true;
                }
            }
            else
            {
                monsterCheck = false;
                Debug.Log(monsterCheck);
            }   
        }
        int layerMask = (1 << LayerMask.NameToLayer("ArrayCheck"));

        if (Input.GetMouseButton(0))
        {
            if (!monsterCheck)
            {
                Vector2 pivotPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D collider = Physics2D.OverlapCircle(pivotPos, CapsuleSize, layerMask);

                if(collider == null)
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

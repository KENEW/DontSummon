using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 MousePosition;
    Camera Camera;
    //public float speed;
    private GameObject target;

    //public Rigidbody2D rigid;
    //Vector2 playerVelocity;

    Vector2 oldPosition;
    Vector2 curPosition;
    Vector2 playerVelocity;

    private void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        oldPosition = transform.position;
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            //CastRay();

            //if (target == this.gameObject)
           // {
                MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);

                if (MousePosition.x > 2.5f)
                {
                    MousePosition.x = 2.5f;
                }

                if (MousePosition.x < -2.5f)
                {
                    MousePosition.x = -2.5f;
                }

                if (MousePosition.y > -2.4f)
                {
                    MousePosition.y = -2.4f;
                }

                if (MousePosition.y < -3.5f)
                {
                    MousePosition.y = -3.5f;
                }

                transform.position = MousePosition;

           // }
        }

        curPosition = transform.position;
        Vector2 distance = curPosition - oldPosition;
        playerVelocity = distance / Time.deltaTime;
        oldPosition = curPosition;

        
        //playerVelocity = rigid.velocity;
    }

    void CastRay() // 유닛 히트처리 부분.  레이를 쏴서 처리합니다. 

    {

        target = null;



        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);



        if (hit.collider != null)
        {

            Debug.Log (hit.collider.name); 

            target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정

        }

    }

    private void OnCollisionEnter2D(Collision2D coll) 
    {
        Debug.Log(playerVelocity);
        coll.gameObject.GetComponent<Rigidbody2D>().AddForce(playerVelocity*10);

    }





}
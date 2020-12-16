using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 MousePosition;
    Camera Camera;

    private SpriteRenderer playerRenderer;
    public Rigidbody2D rigid;
    //Vector2 playerVelocity;


    //Vector2 oldPosition;
    //Vector2 curPosition;
    //Vector2 playerVelocity;



    public Sprite[] faceSprite;

    private void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        playerRenderer = gameObject.GetComponent<SpriteRenderer>();

        //oldPosition = transform.position;
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
                MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);

            
                if (MousePosition.x > 2.4f)
                {
                    MousePosition.x = 2.4f;
                }

                if (MousePosition.x < -2.4f)
                {
                    MousePosition.x = -2.4f;
                }

                if (MousePosition.y > -2.75f)
                {
                    MousePosition.y = -2.75f;
                }

                if (MousePosition.y < -3.75f)
                {
                    MousePosition.y = -3.75f;
                }

                //transform.position = MousePosition;
                rigid.MovePosition(MousePosition);

           // }
        }

        //curPosition = transform.position;
        //Vector2 distance = curPosition - oldPosition;
        //playerVelocity = distance / Time.deltaTime;
        //oldPosition = curPosition;

        
        //playerVelocity = rigid.velocity;
    }



    IEnumerator Face(Sprite changeSprite)
    {
        playerRenderer.sprite = changeSprite;
        yield return new WaitForSeconds(1f);
        playerRenderer.sprite = faceSprite[0];
    }

    public void ChangeFace(int num)
    {
        StartCoroutine(Face(faceSprite[num]));
    }



}
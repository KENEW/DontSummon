using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 MousePosition;
    Camera Camera;

    private void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);

            if(MousePosition.x > 2.5f)
            {
                MousePosition.x = 2.5f;
            }

            if (MousePosition.x < -2.5f)
            {
                MousePosition.x = -2.5f;
            }

            if (MousePosition.y > -2.7f)
            {
                MousePosition.y = -2.7f;
            }

            if (MousePosition.y < -4f)
            {
                MousePosition.y = -4f;
            }

            transform.position = MousePosition;

        }
    }

    

}
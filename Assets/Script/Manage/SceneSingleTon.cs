using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    void Awake()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(T)) as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static T Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }

            return instance;
        }
    }
}
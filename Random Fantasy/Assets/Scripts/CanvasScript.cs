using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    void Awake()
    {
        //if (GameObject.Find("Canvas") == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
}

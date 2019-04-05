using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Status : MonoBehaviour
{
    public static Status status { get; set; }

    Dictionary<string, int> envStatus = new Dictionary<string, int>();
    public Dictionary<string, int> EnvStatus { get => envStatus; set => envStatus = value; }

    void Awake()
    {
        if (status == null)
        {
            status = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Populate the dictionary
        envStatus.Add("ShopKeeper", 0);
    }

    void Update()
    {

    }
}

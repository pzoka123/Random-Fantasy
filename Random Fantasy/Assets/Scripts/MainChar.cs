using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    public static MainChar mainChar { get; set; }

    Dictionary<string, int> inventory = new Dictionary<string, int>();
    public Dictionary<string, int> Inventory { get => inventory; set => inventory = value; }

    void Awake()
    {
        if (mainChar == null)
        {
            mainChar = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

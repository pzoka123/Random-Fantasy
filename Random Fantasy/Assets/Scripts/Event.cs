using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    GameObject eventCard;
    GameObject[] choices;

    string eventName;
    public string EventName { get => eventName; set => eventName = value; }

    string[] sentences;
    public string[] Sentences { get => sentences; set => sentences = value; }

    // Start is called before the first frame update
    void Start()
    {
        eventCard = GameObject.FindGameObjectWithTag("EventCard");
        choices = GameObject.FindGameObjectsWithTag("Choice");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display()
    {

    }

    public void Hide()
    {

    }
}

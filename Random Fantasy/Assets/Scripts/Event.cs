using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    GameObject[] choices;

    string eventName;
    public string EventName { get => eventName; set => eventName = value; }

    string[] sentences;
    public string[] Sentences { get => sentences; set => sentences = value; }

    // Start is called before the first frame update
    void Start()
    {
        choices = GameObject.FindGameObjectsWithTag("ChoiceCard");
    }

    public void Clicked()
    {
        gameObject.GetComponent<Animator>().SetBool("isActive", true);
        GameLoop.gameLoop.textFile = Resources.Load("Texts/MysteriousTree") as TextAsset;
        GameLoop.gameLoop.dialogueStart = true;
    }
}

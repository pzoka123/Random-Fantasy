using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    string choiceName;
    public string ChoiceName { get => choiceName; set => choiceName = value; }

    string[] sentences;
    public string[] Sentences { get => sentences; set => sentences = value; }

    public void Clicked()
    {
        gameObject.GetComponent<Animator>().SetBool("isActive", true);
        EventManager.eventManager.HideEvent();
        EventManager.eventManager.currClicked = gameObject;
        EventManager.eventManager.HideChoice();
        GameLoop.gameLoop.textFile = Resources.Load("Dialogues/" + gameObject.name) as TextAsset;
        //GameLoop.gameLoop.dialogueStart = true;
        GameLoop.gameLoop.isEvent = false;
        GameLoop.gameLoop.isDialogue = true;
    }
}

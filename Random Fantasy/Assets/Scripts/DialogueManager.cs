﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager { get; set; }

    public List<DialoguePart> dialogues = new List<DialoguePart>();
    //public string[,] dialogues;
    int currDialog = 0;

    string dialogName;
    Queue<string> sentences = new Queue<string>();

    GameObject nameBox;
    GameObject dialogBox;

    public string nextAction;
    public string nextEvent;

    public bool combat;
    public bool end;
    public bool action;

    void Awake()
    {
        if (dialogueManager == null)
        {
            dialogueManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        nameBox = GameObject.FindGameObjectWithTag("NameBox");
        dialogBox = GameObject.FindGameObjectWithTag("DialogBox");
    }

    public void Display()
    {
        sentences.Clear();

        Setup(currDialog);

        if (dialogName == "")
            nameBox.SetActive(false);
        else
        {
            nameBox.SetActive(true);
        }

        dialogBox.GetComponent<Animator>().SetBool("isActive", true);

        DisplayNextSentence();
    }

    public void Hide()
    {
        dialogBox.GetComponent<Animator>().SetBool("isActive", false);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            currDialog++;
            if (currDialog != dialogues.Count)
            {
                Display();
            }
            else
            {
                //GameLoop.gameLoop.dialogueEnd = true;
                GameLoop.gameLoop.isDialogue = false;
                if (GameLoop.gameLoop.eventPhase)
                {
                    if (EventManager.eventManager.currClicked != null)
                    {
                        if (EventManager.eventManager.currClicked.tag == "EventCard")
                        {
                            GameLoop.gameLoop.isEvent = true;
                            EventManager.eventManager.ShowChoice();
                            EventManager.eventManager.ReturnEvent();
                        }
                        else if (EventManager.eventManager.currClicked.tag == "ChoiceCard")
                        {
                            GameLoop.gameLoop.isAction = true;
                            EventManager.eventManager.ReturnChoice();
                            EventManager.eventManager.Hide();
                            GameLoop.gameLoop.eventPhase = false;
                        }
                    }
                }
                else if (combat)
                {
                    GameLoop.gameLoop.isCombat = true;
                    combat = false;
                }
                else if (action)
                {
                    GameLoop.gameLoop.isAction = true;
                    action = false;
                }
                currDialog = 0;
                return;
            }
        }
        else
        {
            string sentence = sentences.Dequeue();
            dialogBox.transform.GetChild(0).GetComponent<Text>().text = sentence;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogBox.transform.GetChild(0).GetComponent<Text>().text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogBox.transform.GetChild(0).GetComponent<Text>().text += letter;
            yield return null;
        }
    }

    void Setup(int dialogueIndex)
    {
        dialogName = dialogues[dialogueIndex].dialogName;
        nameBox.transform.GetChild(0).GetComponent<Text>().text = dialogName;
        
        string[] lines = dialogues[dialogueIndex].dialogSentences.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < lines.Length; i++)
        {
            sentences.Enqueue(lines[i]);
        }
    }
}

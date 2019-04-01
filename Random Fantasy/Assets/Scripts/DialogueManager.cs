using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager { get; set; }

    public List<DialoguePart> dialogues = new List<DialoguePart>();
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
        dialogName = null;
        sentences.Clear();
        
        Setup(currDialog);
        
        if (dialogName == "" || dialogName == null)
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
                if (GameLoop.gameLoop.currentAction == GameLoop.Actions.eventAction)
                {
                    GameLoop.gameLoop.eventDescDialogue = false;
                }
                else
                {
                    GameLoop.gameLoop.currentAction = GameLoop.Actions.standby;
                    GameLoop.gameLoop.currentFile = GameLoop.gameLoop.nextFile;
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

        List<string> lines = dialogues[dialogueIndex].dialogSentences;
        for (int i = 0; i < lines.Count; i++)
        {
            sentences.Enqueue(lines[i]);
        }
    }
}

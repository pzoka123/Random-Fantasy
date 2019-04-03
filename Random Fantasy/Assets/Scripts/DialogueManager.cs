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
    string currentSentence;

    GameObject nameBox;
    GameObject dialogBox;
    bool runningText = false;

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
        EventManager.eventManager.eventCard.GetComponent<Button>().enabled = false;
        if (EventManager.eventManager.currClicked != null)
            EventManager.eventManager.currClicked.GetComponent<Button>().enabled = false;
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
        if (sentences.Count == 0 && !runningText)
        {
            currDialog++;
            if (currDialog != dialogues.Count)
            {
                Display();
            }
            else
            {
                if (EventManager.eventManager.clicked == "event")
                {
                    EventManager.eventManager.eventCard.GetComponent<Animator>().SetBool("isActive", false);
                    EventManager.eventManager.ShowChoice();
                    EventManager.eventManager.eventCard.GetComponent<Button>().enabled = true;
                }
                else if (EventManager.eventManager.clicked == "choice")
                {
                    EventManager.eventManager.currClicked.GetComponent<Animator>().SetBool("isActive", false);
                    EventManager.eventManager.ReturnChoice();
                    GameLoop.gameLoop.currentAction = GameLoop.Actions.standby;
                    GameLoop.gameLoop.nextAction = GameLoop.Actions.dialogue;
                    EventManager.eventManager.clicked = null;
                    EventManager.eventManager.Hide();
                }

                if (GameLoop.gameLoop.currentAction == GameLoop.Actions.eventAction)
                {
                    GameLoop.gameLoop.eventDescDialogue = false;
                }
                else
                {
                    GameLoop.gameLoop.currentAction = GameLoop.Actions.standby;
                    GameLoop.gameLoop.currentFile = GameLoop.gameLoop.nextFile;
                    if (GameLoop.gameLoop.nextScene != GameLoop.gameLoop.currentScene)
                    {
                        GameLoop.gameLoop.transition = true;
                    }
                }
                Hide();

                currDialog = 0;
                return;
            }
        }
        else
        {
            if (runningText)
            {
                StopAllCoroutines();
                dialogBox.transform.GetChild(0).GetComponent<Text>().text = currentSentence;
                runningText = false;
            }
            else
            {
                currentSentence = sentences.Dequeue();
                StopAllCoroutines();
                StartCoroutine(TypeSentence(currentSentence));
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        runningText = true;
        dialogBox.transform.GetChild(0).GetComponent<Text>().text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogBox.transform.GetChild(0).GetComponent<Text>().text += letter;
            yield return null;
        }
        runningText = false;
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

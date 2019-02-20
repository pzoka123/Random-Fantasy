using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager { get; set; }

    public List<DialoguePart> dialogues;
    //public string[,] dialogues;
    int currDialog = 0;

    string dialogName;
    Queue<string> sentences = new Queue<string>();

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

    public void Display()
    {
        sentences.Clear();

        Setup(currDialog);

        if (dialogName == "")
            GameObject.FindGameObjectWithTag("NameBox").SetActive(false);
        else
        {
            GameObject.FindGameObjectWithTag("NameBox").SetActive(true);
        }

        GameObject.FindGameObjectWithTag("DialogBox").GetComponent<Animator>().SetBool("isActive", true);

        DisplayNextSentence();
    }

    public void Hide()
    {
        GameObject.FindGameObjectWithTag("DialogBox").GetComponent<Animator>().SetBool("isActive", false);
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
                GameLoop.gameLoop.dialogueEnd = true;
                return;
            }
        }
        else
        {
            string sentence = sentences.Dequeue();
            GameObject.FindGameObjectWithTag("DialogBox").transform.GetChild(0).GetComponent<Text>().text = sentence;
        }
        //StopAllCoroutines();
        //StartCoroutine(TypeSentence(sentence, dialogueText));
    }

    //IEnumerator TypeSentence(string sentence, Text dialogueT)
    //{
    //    dialogueT.text = "";
    //    foreach (char letter in sentence.ToCharArray())
    //    {
    //        dialogueT.text += letter;
    //        yield return null;
    //    }
    //}

    void Setup(int dialogueIndex)
    {
        dialogName = dialogues[dialogueIndex].dialogName;
        GameObject.FindGameObjectWithTag("NameBox").transform.GetChild(0).GetComponent<Text>().text = dialogName;

        string[] lines = dialogues[dialogueIndex].dialogSentence.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < lines.Length; i++)
        {
            sentences.Enqueue(lines[i]);
        }
    }
}

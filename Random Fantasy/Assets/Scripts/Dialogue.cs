using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    Master gameManager;
    public string dialogName;

    public string key;

    [TextArea(3,10)]
    public string[] sentences;
    
    public bool isClicked;

    public Queue<string> sentencesQueue;

    Animator anim;

    DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<Master>();
        sentencesQueue = new Queue<string>();
        anim = gameObject.GetComponent<Animator>();
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        dialogueManager.currCard = gameObject;
        dialogueManager.DialogDisplay(dialogName);

        if (gameObject.name != "Neutral")
            anim.SetBool("isOpen", true);

        sentencesQueue.Clear();
        
        foreach (string sentence in sentences)
        {
            sentencesQueue.Enqueue(sentence);
        }
        dialogueManager.DisplayNextSentence();
    }

    public void EndDialogue()
    {
        if (gameObject.name != "Neutral")
            anim.SetBool("isOpen", false);
        isClicked = false;
        if (gameObject.tag != "EventCard")
        {
            gameObject.SetActive(false);
            if (gameObject.tag == "Fight")
            {
                gameManager.fight = true;
            }
            else if (gameObject.tag == "Blessed")
            {
                gameManager.dice += 1;
                gameManager.fight = true;
            }
            else if (gameObject.tag == "Sleep")
            {
                gameManager.sleep = true;
            }
        }
        else
        {
            dialogueManager.DialogReturn();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] cards;
    GameObject currCard;

    public Text nameText;
    public Text dialogueText;

    public Animator anim;

    Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<DialogueTrigger>().isClicked == false)
            {
                cards[i].SetActive(false);
            }
            else
            {
                cards[i].GetComponent<Animator>().SetBool("isOpen", true);
                currCard = cards[i];
            }
        }

        anim.SetBool("isActive", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    void EndDialogue()
    {
        anim.SetBool("isActive", false);
        if (currCard.tag == "EventCard")
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].GetComponent<DialogueTrigger>().isClicked = false;
                cards[i].GetComponent<Animator>().SetBool("isOpen", false);
                cards[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].GetComponent<DialogueTrigger>().isClicked = false;
                cards[i].SetActive(false);
            }
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}

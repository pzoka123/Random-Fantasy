using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Master gameManager;
    DiceScript diceScript;
    public Text nameText;
    public Text dialogueText;
    public Animator dialogueAnim;

    public GameObject currCard;
    public GameObject[] cards;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<Master>();
        diceScript = GameObject.FindObjectOfType<DiceScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void DisplayNextSentence()
    {
        if (currCard.GetComponent<Dialogue>().sentencesQueue.Count == 0)
        {
            currCard.GetComponent<Dialogue>().EndDialogue();
            dialogueAnim.SetBool("isActive", false);
            if (currCard.GetComponent<Dialogue>().key == "Fighting")
            {
                gameManager.DiceBoardDisplay();
                diceScript.Setup();
            }
            return;
        }
        string sentence = currCard.GetComponent<Dialogue>().sentencesQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, dialogueText));
    }
    
    IEnumerator TypeSentence(string sentence, Text dialogueT)
    {
        dialogueT.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueT.text += letter;
            yield return null;
        }
    }

    public void DialogDisplay(string name)
    {
        if (name == "")
            nameText.rectTransform.parent.gameObject.SetActive(false);
        else
        {
            nameText.rectTransform.parent.gameObject.SetActive(true);
            nameText.text = name;
        }
        dialogueAnim.SetBool("isActive", true);

        foreach(GameObject card in cards)
        {
            if (card != currCard)
            {
                card.SetActive(false);
            }
        }
    }

    public void DialogReturn()
    {
        foreach (GameObject card in cards)
        {
            card.SetActive(true);
        }
    }
}

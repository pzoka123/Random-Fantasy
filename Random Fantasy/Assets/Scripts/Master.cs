using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public int dice = 2;
    public bool fight = false;
    public bool callOnce = true;
    public bool diceBoard = false;
    public Dialogue dialogue;

    public Animator animDiceBoard;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Knight_2").transform.position.x == 6.0f && callOnce)
        {
            Fight();
            callOnce = false;
        }
    }

    void Fight()
    {
        dialogue.dialogName = "Spear Knight";
        dialogue.sentences = new string[3];
        dialogue.sentences[0] = "Stop right there, Golden Knight. I have come to challenge you to a duel.";
        dialogue.sentences[1] = "Today, we will see which is stronger, your sword or my spear.";
        dialogue.sentences[2] = "Prepare yourself!";
        dialogue.key = "Fighting";
        dialogueManager.currCard = dialogue.gameObject;
        dialogue.StartDialogue();
    }

    public void DiceBoardDisplay()
    {
        animDiceBoard.SetBool("isActive", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public int dice = 2;
    public bool fight = false;
    public bool sleep = false;
    public bool dead = false;
    public bool callOnce = true;
    public bool diceBoard = false;
    public Dialogue dialogue;

    public Animator animDiceBoard;

    public Image dark;
    float lerpValue = 0;

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
        if (sleep == true)
        {
            dark.gameObject.SetActive(true);
            Sleep();
            if (dark.color.a == 1)
            {
                dead = true;
                sleep = false;
            }
        }
        if (dead == true)
        {
            Dead();
            dead = false;
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

    void Sleep()
    {
        lerpValue += Time.deltaTime * 1.0f;
        dark.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, lerpValue));
    }

    void Dead()
    {
        dialogue.dialogName = "";
        dialogue.sentences = new string[3];
        dialogue.sentences[0] = "As soon as you fall asleep, a group of bandits appears. It seems they have been following you for a while.";
        dialogue.sentences[1] = "They killed you in your sleep, robbing you off your weapons and golden armor.";
        dialogue.sentences[2] = "You died a pitiful death...";
        dialogue.key = "";
        dialogueManager.currCard = dialogue.gameObject;
        dialogue.StartDialogue();
    }
}

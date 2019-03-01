using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionManager : MonoBehaviour
{
    public static ActionManager actionManager { get; set; }

    public GameObject mainChar;
    public GameObject otherChar;
    
    string[] lines;

    public bool nextDialogue = false;
    public bool nextCombat = false;

    void Awake()
    {
        if (actionManager == null)
        {
            actionManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
    }

    public void ReadText(TextAsset textFile)
    {
        lines = textFile.text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; i++)
        {
            string[] sections = lines[i].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            string sub = sections[0].Substring(1, sections[0].Length - 1);

            if (lines[i][0] == '1')
            {
                if (sub == "in")
                {
                    mainChar.GetComponent<Animator>().SetBool("in", true);
                    mainChar.GetComponent<Character>().walkIn = true;
                }
                else if (sub == "out")
                {
                    mainChar.GetComponent<Animator>().SetBool("in", false);
                }
            }
            else if (lines[i][0] == '2')
            {
                if (sub == "in")
                {
                    otherChar.GetComponent<Animator>().SetBool("in", true);
                    otherChar.GetComponent<Character>().walkIn = true;
                    otherChar.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                }
                else if (sub == "out")
                {
                    otherChar.GetComponent<Animator>().SetBool("in", false);
                }
            }
            else if (lines[i][0] == '3')
            {
                GameLoop.gameLoop.textFile = Resources.Load("Dialogues/" + sub) as TextAsset;
                nextDialogue = true;
            }
            else if (lines[i][0] == '4')
            {
                if (sub == "isDead")
                {
                    GameLoop.gameLoop.isDead = true;
                }
            }
        }
    }

    public void WalkIn()
    {
        //mainChar.GetComponent<Character>().CharWalkIn();
        otherChar.GetComponent<Character>().CharWalkIn();

        //if (!mainChar.GetComponent<Character>().walkIn && !otherChar.GetComponent<Character>().walkIn)
        if (!otherChar.GetComponent<Character>().walkIn)
        {
            if (nextDialogue)
            {
                GameLoop.gameLoop.isAction = false;
                GameLoop.gameLoop.isDialogue = true;
                nextDialogue = false;
            }
        }
    }

    public void AttackMain()
    {
        mainChar.GetComponent<Character>().CharAttack(otherChar);
        //if (!otherChar.GetComponent<Character>().walkIn)
        //{
        //    if (nextDialogue)
        //    {
        //        GameLoop.gameLoop.isAction = false;
        //        GameLoop.gameLoop.isDialogue = true;
        //    }
        //}
    }

    public void AttackOther()
    {
        otherChar.GetComponent<Character>().CharAttack(mainChar);
        //if (!otherChar.GetComponent<Character>().walkIn)
        //{
        //    if (nextDialogue)
        //    {
        //        GameLoop.gameLoop.isAction = false;
        //        GameLoop.gameLoop.isDialogue = true;
        //    }
        //}
    }

    public void Die()
    {
        mainChar.GetComponent<Character>().CharDie();
        otherChar.GetComponent<Character>().CharDie();
    }

    public void NextDialogue()
    {
        if (nextDialogue)
        {
            GameLoop.gameLoop.isCombat = false;
            GameLoop.gameLoop.isDialogue = true;
            nextDialogue = false;
        }
    }

    public void Death()
    {
        if (GameLoop.gameLoop.darkVal < 1)
        {
            GameLoop.gameLoop.dark.SetActive(true);
            GameLoop.gameLoop.darkVal += Time.deltaTime / 1.5f;
            GameLoop.gameLoop.dark.GetComponent<Image>().color = new Color(0, 0, 0, Mathf.Lerp(0.0f, 1.0f, GameLoop.gameLoop.darkVal));
        }
        else
        {
            GameLoop.gameLoop.isDead = false;
        }
    }
}

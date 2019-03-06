using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionManager : MonoBehaviour
{
    public static ActionManager actionManager { get; set; }

    GameObject mainChar;
    public GameObject MainChar { get => mainChar; set => mainChar = value; }

    GameObject otherChar;
    public GameObject OtherChar { get => otherChar; set => otherChar = value; }

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
        otherChar = GameObject.FindGameObjectWithTag("OtherChar");
    }

    void Update()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        otherChar = GameObject.FindGameObjectWithTag("OtherChar");
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
                if (sub == "walk")
                {
                    MainChar.GetComponent<Animator>().SetBool("walk", true);
                    MainChar.GetComponent<Character>().move = true;
                    MainChar.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        MainChar.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        MainChar.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (sub == "run")
                {
                    MainChar.GetComponent<Animator>().SetBool("run", true);
                    MainChar.GetComponent<Character>().move = true;
                    MainChar.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        MainChar.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        MainChar.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
            }
            else if (lines[i][0] == '2')
            {
                if (sub == "walk")
                {
                    OtherChar.GetComponent<Animator>().SetBool("walk", true);
                    OtherChar.GetComponent<Character>().move = true;
                    OtherChar.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        OtherChar.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        OtherChar.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (sub == "run")
                {
                    OtherChar.GetComponent<Animator>().SetBool("run", true);
                    OtherChar.GetComponent<Character>().move = true;
                    OtherChar.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        OtherChar.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        OtherChar.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
            }
            else if (lines[i][0] == '3')
            {
                GameLoop.gameLoop.textFile = Resources.Load("Dialogues/" + sub) as TextAsset;
                //if (sections[1] == "event")
                //{
                //    GameLoop.gameLoop.isEvent = true;
                //    GameLoop.gameLoop.eventPhase = true;
                //}
                nextDialogue = true;
            }
            else if (lines[i][0] == '4')
            {
                if (sub == "isDead")
                {
                    GameLoop.gameLoop.isDead = true;
                }
            }
            else if (lines[i][0] == '5')
            {
                GameLoop.gameLoop.endScene = true;
                GameLoop.gameLoop.nextScene = sub;
                GameLoop.gameLoop.isAction = true;
                DialogueManager.dialogueManager.nextAction = sections[1];
                //GameLoop.gameLoop.FadeOut();
                //GameLoop.gameLoop.LoadScene(sub);
            }
            else if (lines[i][0] == '6')
            {
                GameLoop.gameLoop.victoryText = sub;
                GameLoop.gameLoop.defeatText = sections[1];
            }
        }
    }

    public void Move()
    {
        MainChar.GetComponent<Character>().CharMove();
        OtherChar.GetComponent<Character>().CharMove();
        
        if (!MainChar.GetComponent<Character>().move && !OtherChar.GetComponent<Character>().move)
        {
            if (nextDialogue)
            {
                GameLoop.gameLoop.isAction = false;
                if (!GameLoop.gameLoop.endScene)
                    GameLoop.gameLoop.isDialogue = true;
                else
                    GameLoop.gameLoop.isDialogue = false;
                nextDialogue = false;
            }
        }
    }

    public void AttackMain()
    {
        MainChar.GetComponent<Character>().CharAttack(OtherChar);
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
        OtherChar.GetComponent<Character>().CharAttack(MainChar);
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
        MainChar.GetComponent<Character>().CharDie();
        OtherChar.GetComponent<Character>().CharDie();
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
}

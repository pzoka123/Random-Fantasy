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
                if (sub == "walk")
                {
                    mainChar.GetComponent<Animator>().SetBool("walk", true);
                    mainChar.GetComponent<Character>().move = true;
                    mainChar.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        mainChar.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        mainChar.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (sub == "run")
                {
                    mainChar.GetComponent<Animator>().SetBool("run", true);
                    mainChar.GetComponent<Character>().move = true;
                    mainChar.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        mainChar.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        mainChar.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
            }
            else if (lines[i][0] == '2')
            {
                if (sub == "walk")
                {
                    otherChar.GetComponent<Animator>().SetBool("walk", true);
                    otherChar.GetComponent<Character>().move = true;
                    otherChar.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        otherChar.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        otherChar.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (sub == "run")
                {
                    otherChar.GetComponent<Animator>().SetBool("run", true);
                    otherChar.GetComponent<Character>().move = true;
                    otherChar.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        otherChar.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        otherChar.GetComponent<SpriteRenderer>().flipX = false;
                    }
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
            else if (lines[i][0] == '5')
            {
                GameLoop.gameLoop.endScene = true;
                GameLoop.gameLoop.nextScene = sub;
                //GameLoop.gameLoop.FadeOut();
                //GameLoop.gameLoop.LoadScene(sub);
            }
        }
    }

    public void Move()
    {
        mainChar.GetComponent<Character>().CharMove();
        otherChar.GetComponent<Character>().CharMove();
        
        if (!mainChar.GetComponent<Character>().move && !otherChar.GetComponent<Character>().move)
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
}

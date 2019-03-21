using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionManager : MonoBehaviour
{
    public static ActionManager actionManager { get; set; }

    GameObject mainCharacter;
    public GameObject MainCharacter { get => mainCharacter; set => mainCharacter = value; }

    GameObject otherCharacter;
    public GameObject OtherCharacter { get => otherCharacter; set => otherCharacter = value; }

    string[] lines;

    public bool nextDialogue = false;
    public bool nextCombat = false;

    bool required = false;

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
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        otherCharacter = GameObject.FindGameObjectWithTag("OtherChar");
    }

    void Update()
    {
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        otherCharacter = GameObject.FindGameObjectWithTag("OtherChar");
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
                    MainCharacter.GetComponent<Animator>().SetBool("walk", true);
                    MainCharacter.GetComponent<Character>().move = true;
                    MainCharacter.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        MainCharacter.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        MainCharacter.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (sub == "run")
                {
                    MainCharacter.GetComponent<Animator>().SetBool("run", true);
                    MainCharacter.GetComponent<Character>().move = true;
                    MainCharacter.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        MainCharacter.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        MainCharacter.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
            }
            else if (lines[i][0] == '2')
            {
                if (sub == "walk")
                {
                    OtherCharacter.GetComponent<Animator>().SetBool("walk", true);
                    OtherCharacter.GetComponent<Character>().move = true;
                    OtherCharacter.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        OtherCharacter.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        OtherCharacter.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (sub == "run")
                {
                    OtherCharacter.GetComponent<Animator>().SetBool("run", true);
                    OtherCharacter.GetComponent<Character>().move = true;
                    OtherCharacter.GetComponent<Character>().posNum = Convert.ToInt32(sections[1]);
                    if (sections[2] == "left")
                    {
                        OtherCharacter.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        OtherCharacter.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
            }
            else if (lines[i][0] == '3')
            {
                if (sections.Length <= 1)
                {
                    GameLoop.gameLoop.textFile = Resources.Load("Dialogues/" + sub) as TextAsset;
                }
                else
                {
                    if (required == false)
                    {
                        if (sections[1] == "inventory")
                        {
                            if (MainChar.mainChar.Inventory.ContainsKey(sections[2]))
                            {
                                if (MainChar.mainChar.Inventory[sections[2]] >= Convert.ToInt32(sections[3]))
                                {
                                    GameLoop.gameLoop.textFile = Resources.Load("Dialogues/" + sub) as TextAsset;
                                    required = true;
                                }
                            }
                            else
                            {
                                GameLoop.gameLoop.textFile = Resources.Load("Dialogues/" + sections[4]) as TextAsset;
                            }
                        }
                        else if (sections[1] == "envStatus")
                        {
                            if (Status.status.EnvStatus[sections[2]] >= Convert.ToInt32(sections[3]))
                            {
                                GameLoop.gameLoop.textFile = Resources.Load("Dialogues/" + sub) as TextAsset;
                                required = true;
                            }
                            else
                            {
                                GameLoop.gameLoop.textFile = Resources.Load("Dialogues/" + sections[4]) as TextAsset;
                            }
                        }
                    }
                }
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
        MainCharacter.GetComponent<Character>().CharMove();
        OtherCharacter.GetComponent<Character>().CharMove();
        
        if (!MainCharacter.GetComponent<Character>().move && !OtherCharacter.GetComponent<Character>().move)
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
        MainCharacter.GetComponent<Character>().CharAttack(OtherCharacter);
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
        OtherCharacter.GetComponent<Character>().CharAttack(MainCharacter);
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
        MainCharacter.GetComponent<Character>().CharDie();
        OtherCharacter.GetComponent<Character>().CharDie();
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

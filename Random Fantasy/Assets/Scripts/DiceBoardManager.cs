using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceBoardManager : MonoBehaviour
{
    public static DiceBoardManager diceBoardManager { get; set; }

    GameObject diceBoard;
    GameObject rollButton;
    GameObject[] dice;

    public Sprite[] diceSprites;
    public bool rolled = false;

    GameObject player;

    void Awake()
    {
        if (diceBoardManager == null)
        {
            diceBoardManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        diceBoard = GameObject.FindGameObjectWithTag("DiceBoard");
        rollButton = GameObject.FindGameObjectWithTag("RollButton");
        dice = GameObject.FindGameObjectsWithTag("Dice");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Display()
    {
        rollButton.SetActive(true);
        diceBoard.GetComponent<Animator>().SetBool("isActive", true);
    }

    public void Hide()
    {
        rollButton.SetActive(true);
        diceBoard.GetComponent<Animator>().SetBool("isActive", false);
    }

    public void Roll()
    {
        rollButton.SetActive(false);
        foreach (GameObject die in dice)
        {
            die.GetComponent<Image>().overrideSprite = null;
            die.GetComponent<Animator>().SetBool("roll", true);
        }

    }

    public void Result()
    {
        int attack = 0;
        int defend = 0;
        int magic = 0;

        foreach (GameObject die in dice)
        {
            if (die.GetComponent<Image>().overrideSprite.name == "SwordDice")
            {
                attack++;
            }
            else if (die.GetComponent<Image>().overrideSprite.name == "ShieldDice")
            {
                defend++;
            }
            else if (die.GetComponent<Image>().overrideSprite.name == "StarDice")
            {
                magic++;
            }
        }

        if (attack >= 3)
        {
            player.GetComponent<Character>().CharAttack();
        }
        else if (defend >= 3)
        {
            player.GetComponent<Character>().CharDefend();
        }
        else if (magic >= 3)
        {
            player.GetComponent<Character>().CharMagic();
        }
    }
}

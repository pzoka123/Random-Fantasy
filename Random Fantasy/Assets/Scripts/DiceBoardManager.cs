using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceBoardManager : MonoBehaviour
{
    public static DiceBoardManager diceBoardManager { get; set; }

    GameObject diceBoard;
    GameObject rollText;
    GameObject rollButton;
    public Sprite[] diceSprites;

    int rollNum;
    public bool canAtk = false;
    public bool canDie = false;
    public bool canAtk2 = false;
    public bool canDie2 = false;
    public int endFight = 0;

    public int dice = 2;
    GameObject[] activeDice;

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
        rollText = GameObject.FindGameObjectWithTag("RollText");
        rollButton = GameObject.FindGameObjectWithTag("RollButton");
    }

    public void Display()
    {
        rollText.SetActive(false);
        rollButton.SetActive(true);
        diceBoard.GetComponent<Animator>().SetBool("isActive", true);
    }

    public void Hide()
    {
        rollText.SetActive(false);
        rollButton.SetActive(true);
        diceBoard.GetComponent<Animator>().SetBool("isActive", false);
    }

    public void Roll()
    {
        rollNum = 0;
        foreach (GameObject die in activeDice)
        {
            int rnd = Random.Range(1, 7);
            rollNum += rnd;
            die.GetComponent<Image>().sprite = diceSprites[rnd - 1];
        }
        Result();
    }

    public void Result()
    {
        string isHit;
        if (rollNum <= 7)
        {
            isHit = "The opponent gets the upper hand and strikes you first.";
        }
        else
        {
            isHit = "You get to strike first.";
        }
        rollText.GetComponent<Text>().text = rollNum.ToString() + ". " + isHit;
        rollText.SetActive(true);
        rollButton.SetActive(false);
    }

    public void Setup()
    {
        activeDice = new GameObject[dice];
        for (int i = 0; i < dice; i++)
        {
            diceBoard.transform.GetChild(i).gameObject.SetActive(true);
            activeDice[i] = diceBoard.transform.GetChild(i).gameObject;
        }
    }
}

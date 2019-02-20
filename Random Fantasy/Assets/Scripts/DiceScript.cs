using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    public Sprite[] diceSprites;
    public GameObject rollText;
    public GameObject rollButton;

    int rollNum;
    public bool canAtk = false;
    public bool canDie = false;
    public bool canAtk2 = false;
    public bool canDie2 = false;
    public int endFight = 0;

    GameObject[] activeDice;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        //activeDice = new GameObject[gameManager.dice];
        //for (int i = 0; i < gameManager.dice; i++)
        //{
        //    gameObject.transform.GetChild(i).gameObject.SetActive(true);
        //    activeDice[i] = gameObject.transform.GetChild(i).gameObject;
        //}
    }

    public void Roll()
    {
        rollNum = 0;
        foreach (GameObject die in activeDice)
        {
            int rnd = Random.Range(1, 7);
            rollNum += rnd;
            die.GetComponent<Image>().sprite = diceSprites[rnd-1];
        }
        Result();
    }

    public void Result()
    {
        string isHit;
        if (rollNum <= 7)
        {
            isHit = "It's a miss.";
            canAtk = false;
            canAtk2 = true;
        }
        else
        {
            isHit = "It's a hit.";
            canAtk = true;
            canAtk2 = false;
        }
        rollText.GetComponent<Text>().text = rollNum.ToString() + ". " + isHit;
        rollText.SetActive(true);
        rollButton.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    Master gameManager;
    public Sprite[] diceSprites;

    GameObject[] activeDice;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<Master>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        activeDice = new GameObject[gameManager.dice];
        for (int i = 0; i < gameManager.dice; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
            activeDice[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    public void Roll()
    {
        foreach (GameObject die in activeDice)
        {
            int rnd = Random.Range(1, 7);
            Debug.Log("random num: " + rnd);
            die.GetComponent<Image>().sprite = diceSprites[rnd-1];
        }
    }
}

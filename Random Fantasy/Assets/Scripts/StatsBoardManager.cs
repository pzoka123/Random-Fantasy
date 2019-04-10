using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBoardManager : MonoBehaviour
{
    public static StatsBoardManager statsBoardManager { get; set; }

    GameObject statsBoard;
    GameObject statsButton;

    GameObject strength;
    GameObject defense;
    GameObject knowledge;
    GameObject alchemy;
    GameObject smithing;

    void Awake()
    {
        if (statsBoardManager == null)
        {
            statsBoardManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        statsBoard = GameObject.FindGameObjectWithTag("StatsBoard");
        statsButton = GameObject.FindGameObjectWithTag("StatsButton");
        strength = GameObject.Find("Strength");
        defense = GameObject.Find("Defense");
        knowledge = GameObject.Find("Knowledge");
        alchemy = GameObject.Find("Alchemy");
        smithing = GameObject.Find("Smithing");
    }

    void Update()
    {
        strength.GetComponent<Text>().text = "Strength " + Player.stats["Strength"];
        defense.GetComponent<Text>().text = "Defense " + Player.stats["Defense"];
        knowledge.GetComponent<Text>().text = "Knowledge " + Player.stats["Knowledge"];
        alchemy.GetComponent<Text>().text = "Alchemy " + Player.stats["Alchemy"];
        smithing.GetComponent<Text>().text = "Smithing " + Player.stats["Smithing"];
    }

    public void Display()
    {
        statsBoard.GetComponent<Animator>().SetBool("isActive", true);
        statsButton.SetActive(false);
    }

    public void Hide()
    {
        statsBoard.GetComponent<Animator>().SetBool("isActive", false);
        statsButton.SetActive(true);
    }
}

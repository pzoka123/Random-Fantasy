using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager combatManager { get; set; }

    public bool playerDefeat = false;
    public bool playerWin = false;

    GameObject player;
    GameObject otherCharacter;

    void Awake()
    {
        if (combatManager == null)
        {
            combatManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        otherCharacter = GameObject.FindGameObjectWithTag("OtherChar");
    }

    public void Combat()
    {
        if (player.GetComponent<Character>().attack)
        {
            otherCharacter.GetComponent<Character>().CharDie();
            playerWin = true;
        }
        else if (player.GetComponent<Character>().defend)
        {
            otherCharacter.GetComponent<Character>().CharAttack();
            Debug.Log("Player defended against enemy's attack.");
            DiceBoardManager.diceBoardManager.rollButton.SetActive(true);
        }
        else if (player.GetComponent<Character>().magic)
        {
            otherCharacter.GetComponent<Character>().CharDie();
            Debug.Log("Player used magic to defeat enemy.");
            playerWin = true;
        }
        else
        {
            otherCharacter.GetComponent<Character>().CharAttack();
            player.GetComponent<Character>().CharDie();
            playerDefeat = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    Event eventCard;
    Dialogue dialogue;
    DiceScript diceBoard;

    bool eventStart;
    bool eventEnd;
    bool dialogueStart;
    bool dialogueEnd;
    bool combatStart;
    bool combatEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eventStart)
        {
            eventCard.Display();
            eventStart = false;
        }
        if (eventEnd)
        {
            eventCard.Hide();
            eventEnd = false;
        }
        //if (dialogueStart)
        //{
        //    eventCard.Display();
        //    eventStart = false;
        //}
        //if (dialogueEnd)
        //{
        //    eventCard.Hide();
        //    eventEnd = false;
        //}
        //if (combatStart)
        //{
        //    eventCard.Display();
        //    eventStart = false;
        //}
        //if (combatEnd)
        //{
        //    eventCard.Hide();
        //    eventEnd = false;
        //}
    }
}

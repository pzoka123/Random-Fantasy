using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public TextAsset textFile;

    public static GameLoop gameLoop { get; set; }

    Event eventCard;
    DiceScript diceBoard;

    public bool eventStart;
    public bool eventEnd;
    public bool dialogueStart;
    public bool dialogueEnd;
    public bool combatStart;
    public bool combatEnd;

    void Awake()
    {
        if (gameLoop == null)
        {
            gameLoop = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (eventStart)
        //{
        //    eventCard.Display();
        //    eventStart = false;
        //}
        //if (eventEnd)
        //{
        //    eventCard.Hide();
        //    eventEnd = false;
        //}
        if (dialogueStart)
        {
            gameObject.GetComponent<Dialogue>().ReadText(textFile);
            dialogueStart = false;
            DialogueManager.dialogueManager.Display();
        }
        if (dialogueEnd)
        {
            dialogueEnd = false;
            DialogueManager.dialogueManager.Hide();
        }
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

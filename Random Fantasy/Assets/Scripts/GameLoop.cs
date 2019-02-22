using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public IEnumerable gameState;

    public TextAsset textFile;

    public static GameLoop gameLoop { get; set; }
    
    DiceBoardManager diceBoard;

    public bool eventStart;
    public bool eventEnd;
    public bool dialogueStart;
    public bool dialogueEnd;
    public bool combatStart;
    public bool combatEnd;

    public bool isEvent;
    public bool isDialogue;
    public bool isAction;
    public bool isCombat;

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

    void Start()
    {
        gameState = EventState();
        StartCoroutine(RunGameLoop());
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
        if (combatStart)
        {
            diceBoard.Display();
            combatStart = false;
        }
        if (combatEnd)
        {
            diceBoard.Hide();
            combatEnd = false;
        }
    }

    public IEnumerator RunGameLoop()
    {
        while (gameState != null)
        {
            foreach (IEnumerable currState in gameState)
            {
                yield return currState;
            }
        }
    }

    public IEnumerable EventState()
    {
        while (true)
        {
            yield return null;
        }
    }

    public IEnumerable DialogueState()
    {
        dialogueStart = true;
        gameObject.GetComponent<Dialogue>().ReadText(textFile);
        DialogueManager.dialogueManager.Display();
        while (dialogueStart)
        {
            yield return null;
        }

        DialogueManager.dialogueManager.Hide();

        gameState = ActionState();
    }

    public IEnumerable ActionState()
    {
        while (true)
        {
            yield return null;
        }
    }

    public IEnumerable CombatState()
    {
        while (true)
        {
            yield return null;
        }
    }
}

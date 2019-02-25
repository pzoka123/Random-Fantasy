using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public IEnumerable gameState;

    public TextAsset textFile;

    public static GameLoop gameLoop { get; set; }

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
        isEvent = true;
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
            DiceBoardManager.diceBoardManager.Display();
            combatStart = false;
        }
        if (combatEnd)
        {
            DiceBoardManager.diceBoardManager.Hide();
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
        EventManager.eventManager.Display();
        while (isEvent)
        {
            yield return null;
        }

        if (isAction)
            gameState = ActionState();
        else if (isDialogue)
            gameState = DialogueState();
        EventManager.eventManager.Hide();
    }

    public IEnumerable DialogueState()
    {
        dialogueStart = true;
        gameObject.GetComponent<Dialogue>().ReadText(textFile);
        DialogueManager.dialogueManager.Display();
        while (isDialogue)
        {
            yield return null;
        }

        DialogueManager.dialogueManager.Hide();

        if (isAction)
            gameState = ActionState();
        else if (isEvent)
            gameState = EventState();
    }

    public IEnumerable ActionState()
    {
        while (isAction)
        {
            yield return null;
        }

        if (isDialogue)
            gameState = ActionState();
        else if (isEvent)
            gameState = EventState();
        else if (isCombat)
            gameState = CombatState();
    }

    public IEnumerable CombatState()
    {
        DiceBoardManager.diceBoardManager.Display();
        while (isCombat)
        {
            yield return null;
        }

        if (isAction)
            gameState = ActionState();
        else if (isDialogue)
            gameState = DialogueState();

        DiceBoardManager.diceBoardManager.Hide();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public IEnumerable gameState;

    public TextAsset textFile;

    public GameObject dark;

    public static GameLoop gameLoop { get; set; }

    //public bool eventStart;
    //public bool eventEnd;
    //public bool dialogueStart;
    //public bool dialogueEnd;
    //public bool combatStart;
    //public bool combatEnd;

    public bool isEvent;
    public bool isDialogue;
    public bool isAction;
    public bool isCombat;
    public bool isEnd;

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
        EventManager.eventManager.eventPhase = true;
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
        //if (dialogueStart)
        //{
        //    gameObject.GetComponent<Dialogue>().ReadText(textFile);
        //    dialogueStart = false;
        //    DialogueManager.dialogueManager.Display();
        //}
        //if (dialogueEnd)
        //{
        //    dialogueEnd = false;
        //    DialogueManager.dialogueManager.Hide();
        //}
        //if (combatStart)
        //{
        //    DiceBoardManager.diceBoardManager.Display();
        //    combatStart = false;
        //}
        //if (combatEnd)
        //{
        //    DiceBoardManager.diceBoardManager.Hide();
        //    combatEnd = false;
        //}
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
        //EventManager.eventManager.Hide();
    }

    public IEnumerable DialogueState()
    {
        //dialogueStart = true;
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
        else if (isCombat)
            gameState = CombatState();
        else if (isEnd)
            gameState = EndState();
    }

    public IEnumerable ActionState()
    {
        textFile = Resources.Load("Actions/" + DialogueManager.dialogueManager.nextAction) as TextAsset;
        ActionManager.actionManager.ReadText(textFile);
        while (isAction)
        {
            ActionManager.actionManager.WalkIn();
            yield return null;
        }

        if (isDialogue)
            gameState = DialogueState();
        else if (isEvent)
            gameState = EventState();
        else if (isCombat)
            gameState = CombatState();
    }

    public IEnumerable CombatState()
    {
        DiceBoardManager.diceBoardManager.Setup();
        DiceBoardManager.diceBoardManager.Display();
        while (isCombat)
        {
            ActionManager.actionManager.AttackMain();
            ActionManager.actionManager.AttackOther();
            ActionManager.actionManager.Die();
            ActionManager.actionManager.NextDialogue();
            yield return null;
        }

        if (ActionManager.actionManager.otherChar.GetComponent<Character>().die)
        {
            Debug.Log("Victory");
            textFile = Resources.Load("Dialogues/Victory") as TextAsset;
        }
        else
        {
            Debug.Log("Defeat");
            textFile = Resources.Load("Dialogues/Defeat") as TextAsset;
        }

        if (isAction)
            gameState = ActionState();
        else if (isDialogue)
            gameState = DialogueState();

        DiceBoardManager.diceBoardManager.Hide();
    }

    public IEnumerable EndState()
    {
        while (isEnd)
        {

            yield return null;
        }
    }
}

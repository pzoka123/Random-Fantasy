using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using static JsonCreator;

public class GameLoop : MonoBehaviour
{
    public IEnumerable gameState;

    public enum Actions
    {
        eventAction,
        dialogue,
        fight
    }
    public Actions currentAction;
    public Actions nextAction;

    EventData currentEvent;
    DialogueData currentDialogue;

    public TextAsset textFile;
    
    public GameObject dark;
    public float darkVal = 0;

    public static GameLoop gameLoop { get; set; }

    public string nextFile;

    public bool startGame = false;
    public bool eventPhase = false;
    public bool isDead = false;
    public bool endScene = false;
    public string nextScene;

    public bool isEvent = false;
    public bool isDialogue = false;
    public bool isAction = false;
    public bool isCombat = false;
    public bool isEnd = false;

    public string victoryText;
    public string defeatText;

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
        gameState = StandbyState();
        StartCoroutine(RunGameLoop());
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

    public IEnumerable StandbyState()
    {
        while (true)
        {
            if (nextAction == Actions.eventAction)
            {
                currentEvent = LoadEvent(nextFile);
                gameState = EventState();
                currentAction = Actions.eventAction;
                break;
            }
            else if (nextAction == Actions.dialogue)
            {
                currentDialogue = LoadDialogue(nextFile);
                gameState = DialogueState();
                currentAction = Actions.dialogue;
                break;
            }
            yield return null;
        }
    }
    
    public IEnumerable EventState()
    {
        EventManager.eventManager.Display();
        while (true)
        {
            if (nextAction == Actions.dialogue)
            {
                gameState = DialogueState();
                break;
            }
            yield return null;
        }
    }

    public IEnumerable DialogueState()
    {
        DialogueManager.dialogueManager.Display();
        while (true)
        {
            if(nextAction == Actions.eventAction)
            {
                gameState = EventState();
                break;
            }
            yield return null;
        }

        DialogueManager.dialogueManager.Hide();

        gameState = StandbyState();
    }

    public IEnumerable ActionState()
    {
        if (startGame)
        {
            textFile = Resources.Load("Starts/TownStart") as TextAsset;
            startGame = false;
        }
        else
        {
            textFile = Resources.Load("Actions/" + DialogueManager.dialogueManager.nextAction) as TextAsset;
        }
        ActionManager.actionManager.ReadText(textFile);

        while (isAction)
        {
            ActionManager.actionManager.Move();
            yield return null;
        }

        while (endScene)
        {
            FadeOut();
            if (dark.GetComponent<Image>().color.a == 1)
            {
                endScene = false;
                LoadScene(nextScene);
            }
            yield return null;
        }

        if (isDialogue)
            gameState = DialogueState();
        else if (isEvent)
            gameState = EventState();
        else if (isCombat)
            gameState = CombatState();
        else
        {
            isAction = true;
            yield return null;
        }
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

        if (ActionManager.actionManager.OtherCharacter.GetComponent<Character>().isDead)
        {
            textFile = Resources.Load("Dialogues/" + victoryText) as TextAsset;
        }
        else
        {
            textFile = Resources.Load("Dialogues/" + defeatText) as TextAsset;
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
            Debug.Log("END");
            FadeOut();
            yield return null;
        }
    }

    public void FadeOut()
    {
        dark.GetComponent<Animator>().SetBool("fadeOut", true);
    }

    public void FadeIn()
    {
        dark.GetComponent<Animator>().SetBool("fadeOut", false);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        FadeIn();
    }

    DialogueData LoadDialogue(string fileName)
    {
        string dialogueJson = File.ReadAllText(Application.dataPath + "/JSON/Dialogues/" + fileName + ".json");
        DialogueData loadedDialogueData = JsonUtility.FromJson<DialogueData>(dialogueJson);
        return loadedDialogueData;
    }

    EventData LoadEvent(string fileName)
    {
        string eventJson = File.ReadAllText(Application.dataPath + "/JSON/Events" + fileName + ".json");
        EventData loadedEventData = JsonUtility.FromJson<EventData>(eventJson);
        return loadedEventData;
    }
}
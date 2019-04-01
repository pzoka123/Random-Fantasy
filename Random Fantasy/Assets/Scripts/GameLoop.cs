using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameLoop : MonoBehaviour
{
    public IEnumerable gameState;

    public enum Actions
    {
        standby,
        eventAction,
        dialogue,
        fight
    }
    public Actions currentAction;
    public Actions nextAction;
    public bool eventDescDialogue;

    JsonCreator.EventData currentEvent;
    DialogueData currentDialogue;
    
    public GameObject dark;
    public float darkVal = 0;

    public static GameLoop gameLoop { get; set; }

    public string currentFile;
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
        currentFile = "WelcomeToOrtus";
        nextAction = Actions.dialogue;
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
                LoadEvent(currentFile);
                gameState = EventState();
                currentAction = Actions.eventAction;
                break;
            }
            else if (nextAction == Actions.dialogue)
            {
                LoadDialogue(currentFile);
                gameState = DialogueState();
                currentAction = Actions.dialogue;
                break;
            }
            yield return null;
        }
    }
    
    public IEnumerable EventState()
    {
        eventDescDialogue = true;
        DialogueManager.dialogueManager.Display();
        while (eventDescDialogue)
        {
            yield return null;
        }
        DialogueManager.dialogueManager.Hide();

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
            if (currentAction != Actions.dialogue)
            {
                gameState = StandbyState();
                break;
            }
            yield return null;
        }

        DialogueManager.dialogueManager.Hide();
    }

    public IEnumerable CombatState()
    {
        DiceBoardManager.diceBoardManager.Setup();
        DiceBoardManager.diceBoardManager.Display();
        yield return null;
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

    void LoadDialogue(string fileName)
    {
        DialogueManager.dialogueManager.dialogues.Clear();
        string dialogueJson = File.ReadAllText(Application.dataPath + "/JSON/Dialogues/" + fileName + ".json");
        DialogueData loadedDialogueData = JsonUtility.FromJson<DialogueData>(dialogueJson);

        List<DialoguePart> dialoguesTemp = new List<DialoguePart>();
        for (int i = 0; i < loadedDialogueData.dialogue.Count; i++)
        {
            DialoguePart tempPart = new DialoguePart
            {
                dialogName = loadedDialogueData.dialogue[i].dialogueName,
                dialogSentences = loadedDialogueData.dialogue[i].sentences
            };
            dialoguesTemp.Add(tempPart);
        }
        DialogueManager.dialogueManager.dialogues = dialoguesTemp;
        nextFile = loadedDialogueData.next;
        if (loadedDialogueData.nextAction == "event")
        {
            nextAction = Actions.eventAction;
        }
    }

    void LoadEvent(string fileName)
    {
        DialogueManager.dialogueManager.dialogues.Clear();
        string eventJson = File.ReadAllText(Application.dataPath + "/JSON/Events/" + fileName + ".json");
        EventData loadedEventData = JsonUtility.FromJson<EventData>(eventJson);

        EventData eventTemp = new EventData
        {
            eventName = loadedEventData.eventName,
            eventDesc = loadedEventData.eventDesc,
        };
        EventManager.eventManager.currentEvent = eventTemp;

        DialoguePart tempPart = new DialoguePart
        {
            dialogName = null,
            dialogSentences = eventTemp.eventDesc
        };
        DialogueManager.dialogueManager.dialogues.Add(tempPart);

        List<ChoiceData> choicesTemp = new List<ChoiceData>();
        for (int i = 0; i < loadedEventData.choices.Count; i++)
        {
            ChoiceData choiceTemp = new ChoiceData
            {
                choiceName = loadedEventData.choices[i].choiceName,
                choiceDesc = loadedEventData.choices[i].choiceDesc,
                next = loadedEventData.choices[i].next
            };
            choicesTemp.Add(choiceTemp);
        }
        EventManager.eventManager.currentChoices = choicesTemp;
    }
}
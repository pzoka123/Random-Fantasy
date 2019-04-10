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
    public bool cardDisplay;

    EventData currentEvent;
    DialogueData currentDialogue;
    
    public GameObject dark;
    public bool transition = false;

    public static GameLoop gameLoop { get; set; }

    public string currentFile;
    public string nextFile;
    public string currentScene;
    public string nextScene;

    GameObject[] scenes;

    public bool startGame = false;
    public bool eventPhase = false;
    public bool isDead = false;
    public bool endScene = false;

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
        currentScene = "Ortus";
        scenes = GameObject.FindGameObjectsWithTag("Scene");
        foreach (GameObject scene in scenes)
        {
            if (scene.name != currentScene)
            {
                scene.SetActive(false);
            }
        }
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
        if (transition)
        {
            FadeOut();
            while (dark.GetComponent<Image>().color.a != 1)
            {
                yield return null;
            }
            transition = false;
        }
        
        while (true)
        {
            if (dark.GetComponent<Image>().color.a == 1 && !transition)
            {
                currentScene = nextScene;
                foreach (GameObject scene in scenes)
                {
                    if (scene.name != currentScene)
                    {
                        scene.SetActive(false);
                    }
                    else
                    {
                        scene.SetActive(true);
                    }
                }
                FadeIn();
            }
            while (dark.GetComponent<Image>().color.a != 0)
            {
                yield return null;
            }
            break;
        }
        

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
            else if (nextAction == Actions.fight)
            {
                gameState = CombatState();
                currentAction = Actions.fight;
                break;
            }
            yield return null;
        }
    }
    
    public IEnumerable EventState()
    {
        EventManager.eventManager.currClicked = null;
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
            if (cardDisplay)
            {
                DialogueManager.dialogueManager.Display();
                cardDisplay = false;
            }

            if (currentAction == Actions.dialogue)
            {
                gameState = DialogueState();
                break;
            }
            else if (currentAction == Actions.standby)
            {
                gameState = StandbyState();
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

        //DialogueManager.dialogueManager.Hide();
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
        else if (loadedDialogueData.nextAction == "fight")
        {
            nextAction = Actions.fight;
        }
        nextScene = loadedDialogueData.scene;

        if (loadedDialogueData.items.Count > 0)
        {
            foreach (string item in loadedDialogueData.items)
            {
                Player.inventory.Add(item);
            }
        }

        if (loadedDialogueData.stats.Count > 0)
        {
            foreach (StatsData stats in loadedDialogueData.stats)
            {
                if (Player.stats.ContainsKey(stats.statsName))
                {
                    Player.stats[stats.statsName] += stats.value;
                }
                else
                {
                    Player.stats.Add(stats.statsName, stats.value);
                }
            }
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
        EventManager.eventManager.eventCard.GetComponentInChildren<Text>().text = eventTemp.eventName;

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
                next = loadedEventData.choices[i].next,
                scene = loadedEventData.choices[i].scene
            };
            choicesTemp.Add(choiceTemp);
        }
        EventManager.eventManager.currentChoices = choicesTemp;
        for (int i = 0; i < EventManager.eventManager.choiceCards.Length; i++)
        {
            EventManager.eventManager.choiceCards[i].GetComponentInChildren<Text>().text = choicesTemp[i].choiceName;
        }
    }

    void LoadCombat(string filename)
    {

    }
}
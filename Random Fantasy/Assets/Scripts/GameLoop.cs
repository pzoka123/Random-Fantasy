using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public IEnumerable gameState;

    public TextAsset textFile;

    public GameObject dark;
    public float darkVal = 0;

    public static GameLoop gameLoop { get; set; }

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
        gameState = ActionState();
        isAction = true;
        startGame = true;
        StartCoroutine(RunGameLoop());
    }

    // Update is called once per frame
    void Update()
    {

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
        gameObject.GetComponent<Dialogue>().ReadText(textFile);

        while (isDead)
        {
            FadeOut();
            if (dark.GetComponent<Image>().color.a == 1)
                isDead = false;
            yield return null;
        }

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
        if (startGame)
        {
            textFile = Resources.Load("Starts/Seller") as TextAsset;
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

        if (ActionManager.actionManager.OtherChar.GetComponent<Character>().isDead)
        {
            textFile = Resources.Load("Dialogues/Victory") as TextAsset;
        }
        else
        {
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
            Debug.Log("END");
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
}

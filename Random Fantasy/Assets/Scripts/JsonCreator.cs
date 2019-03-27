using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonCreator : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Test reading Event Json
        string eventJson = File.ReadAllText(Application.dataPath + "/JSON/testEventJson.json");
        EventData loadedEventData = JsonUtility.FromJson<EventData>(eventJson);

        //Test reading Dialogue Json
        string dialogueJson = File.ReadAllText(Application.dataPath + "/JSON/testDialogueJson.json");
        DialogueData loadedDialogueData = JsonUtility.FromJson<DialogueData>(dialogueJson);
    }

    class EventData
    {
        public string eventName;
        public string[] eventDesc;
        public IList<ChoiceData> choices;

        public EventData()
        {
            eventName = "Event";
            eventDesc = new string[]
            {
            "This is event line 1",
            "This is event line 2"
            };
            choices = new List<ChoiceData>
            {
                new ChoiceData
                {
                    choiceName = "Choice 1",
                    next = "Next action",
                    choiceDesc = new string[]
                    {
                    "This is choice 1 first line",
                    "This is choice 1 second line"
                    }
                },
                new ChoiceData
                {
                    choiceName = "Choice 2",
                    next = "Next action",
                    choiceDesc = new string[]
                    {
                    "This is choice 2 first line",
                    "This is choice 2 second line"
                    }
                },
                new ChoiceData
                {
                    choiceName = "Choice 3",
                    next = "Next action",
                    choiceDesc = new string[]
                    {
                    "This is choice 3 first line",
                    "This is choice 3 second line"
                    }
                }
            };
        }
    }

    class ChoiceData
    {
        public string choiceName;
        public string[] choiceDesc;
        public string next;

        public ChoiceData()
        {
            choiceName = "Choice";
            next = "Next action";
            choiceDesc = new string[]
            {
                "This is the first line",
                "This is the second line"
            };
        }
    }

    class DialogueData
    {
        public IList<Dialogue> dialogue;
        public string next;

        public DialogueData()
        {
            dialogue = new List<Dialogue>
            {
                new Dialogue
                {
                    name = "Player",
                    sentences = new string[]
                    {
                        "Player sentence 1",
                        "Player sentence 2",
                        "Player sentence 3"
                    }
                },
                new Dialogue
                {
                    name = "Other Char",
                    sentences = new string[]
                    {
                        "Other sentence 1",
                        "Other sentence 2",
                        "Other sentence 3"
                    }
                },
                new Dialogue
                {
                    name = "Player",
                    sentences = new string[]
                    {
                        "Player sentence 4",
                        "Player sentence 5",
                    }
                },
                new Dialogue
                {
                    name = "Other Char",
                    sentences = new string[]
                    {
                        "Other sentence 4",
                        "Other sentence 5",
                        "Other sentence 6"
                    }
                },
                new Dialogue
                {
                    name = "Player",
                    sentences = new string[]
                    {
                        "Player sentence 7"
                    }
                }
            };
            next = "Next action";
        }
    }

    class Dialogue
    {
        public string name;
        public string[] sentences;

        public Dialogue()
        {
            name = "Char Name";
            sentences = new string[]
            {
                "This is the first sentence",
                "This is the second sentence",
                "This is the third sentence"
            };
        }
    }
}

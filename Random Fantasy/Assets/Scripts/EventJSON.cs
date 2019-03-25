using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EventJSON : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventData eventData = new EventData();

        ChoiceData choice1 = new ChoiceData();
        choice1.choiceName = "Choice 1";
        choice1.choiceDesc = new string[]
        {
            "This is choice 1 first line",
            "This is choice 1 second line"
        };

        ChoiceData choice2 = new ChoiceData();
        choice2.choiceName = "Choice 2";
        choice2.choiceDesc = new string[]
        {
            "This is choice 2 first line",
            "This is choice 2 second line"
        };

        ChoiceData choice3 = new ChoiceData();
        choice3.choiceName = "Choice 3";
        choice3.choiceDesc = new string[]
        {
            "This is choice 3 first line",
            "This is choice 3 second line"
        };

        eventData.eventName = "Event";
        eventData.eventDesc = new string[]
        {
            "This is event line 1",
            "This is event line 2"
        };
        eventData.choices = new string[] { choice1.choiceName, choice2.choiceName, choice3.choiceName };

        string json = JsonUtility.ToJson(eventData);
        Debug.Log(json);

        File.WriteAllText(Application.dataPath + "/JSON/testjson.json", json);

        EventData loadedEventData = JsonUtility.FromJson<EventData>(json);
    }

    class EventData
    {
        public string eventName;
        public string[] eventDesc;

        public string[] choices;
    }

    class ChoiceData
    {
        public string choiceName;
        public string[] choiceDesc;

        public string[] condition;
        public string next;
    }
}

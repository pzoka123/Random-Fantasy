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

        string json = File.ReadAllText(Application.dataPath + "/Resources/Dialogues/SampleEvent.json");
        EventData loadedEventData = JsonUtility.FromJson<EventData>(json);
    }

    class EventData
    {
        public string eventName;
        public string[] eventDesc;

        public ChoiceData[] choices;
    }

    class ChoiceData
    {
        public string choiceName;
        public string[] choiceDesc;
    }
}

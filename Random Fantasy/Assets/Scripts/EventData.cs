using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventData
{
    public string eventName;
    public List<string> eventDesc;
    public List<ChoiceData> choices;
}

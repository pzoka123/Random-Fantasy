using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public List<Dialogue> dialogue;
    public string next;
    public string nextAction;
    public string scene;
    public List<string> items;
    public List<StatsData> stats;
}

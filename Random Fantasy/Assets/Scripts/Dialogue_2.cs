using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using static JsonCreator;

public class Dialogue_2 : MonoBehaviour
{
    List<DialoguePart> dialogues = new List<DialoguePart>();

    public void ReadDialogue(string dialogueFile)
    {
        DialogueManager_2.dialogueManager_2.dialogues.Clear();

        string dialogueJson = File.ReadAllText(Application.dataPath + "/JSON/Dialogues/" + dialogueFile);
        DialogueData loadedDialogueData = JsonUtility.FromJson<DialogueData>(dialogueJson);

        for (int i = 0; i < loadedDialogueData.dialogue.Count; i++)
        {
            DialoguePart tempPart = new DialoguePart();
            tempPart.dialogName = loadedDialogueData.dialogue[i].name;
            tempPart.dialogSentences = loadedDialogueData.dialogue[i].sentences;
            dialogues.Add(tempPart);
        }
        DialogueManager.dialogueManager.dialogues = dialogues;
    }
}

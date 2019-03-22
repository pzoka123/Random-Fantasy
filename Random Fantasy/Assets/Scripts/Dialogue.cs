using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Dialogue : MonoBehaviour
{
    List<DialoguePart> dialogues = new List<DialoguePart>();

    //string[,] dialogues = new string[1,2];

    string[] parts;
    string[] lines;

    public void ReadText(TextAsset textFile)
    {
        DialogueManager.dialogueManager.dialogues.Clear();
        parts = textFile.text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < parts.Length; i++)
        {
            DialoguePart tempPart = new DialoguePart();

            lines = parts[i].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < lines.Length; j++)
            {
                string[] sections = lines[j].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                if (lines[j][0] == '1')
                {
                    tempPart.dialogName = lines[j].Substring(1, lines[j].Length - 1);
                }
                else if (lines[j][0] == '2')
                {
                    tempPart.dialogSentence += "_" + lines[j].Substring(1, lines[j].Length - 1);
                }
                else if (lines[j][0] == '3')
                {
                    DialogueManager.dialogueManager.
                        nextAction = sections[0].Substring(1, sections[0].Length - 1);
                    if (sections[1] == "combat")
                    {
                        DialogueManager.dialogueManager.combat = true;
                        DialogueManager.dialogueManager.end = false;
                        DialogueManager.dialogueManager.action = false;
                    }
                    else if (sections[1] == "end")
                    {
                        DialogueManager.dialogueManager.combat = false;
                        DialogueManager.dialogueManager.end = true;
                        DialogueManager.dialogueManager.action = false;
                    }
                    else if (sections[1] == "action")
                    {
                        DialogueManager.dialogueManager.combat = false;
                        DialogueManager.dialogueManager.end = false;
                        DialogueManager.dialogueManager.action = true;
                    }
                }
                else if (lines[j][0] == '4')
                {
                    DialogueManager.dialogueManager.nextEvent = sections[0].Substring(1, sections[0].Length - 1);
                    
                    if (EventManager.eventManager.currClicked == null || EventManager.eventManager.currClicked.name != sections[0].Substring(1, sections[0].Length - 1))
                    {
                        GameObject newEvent = Instantiate(Resources.Load("Events/" + sections[0].Substring(1, sections[0].Length - 1))) as GameObject;
                        GameObject newChoice1 = Instantiate(Resources.Load("Choices/" + sections[1])) as GameObject;
                        GameObject newChoice2 = Instantiate(Resources.Load("Choices/" + sections[2])) as GameObject;
                        GameObject newChoice3 = Instantiate(Resources.Load("Choices/" + sections[3])) as GameObject;

                        newEvent.name = sections[0].Substring(1, sections[0].Length - 1);
                        newChoice1.name = sections[1];
                        newChoice2.name = sections[2];
                        newChoice3.name = sections[3];

                        newEvent.transform.SetParent(GameObject.FindGameObjectWithTag("EventBoard").transform, false);
                        newChoice1.transform.SetParent(GameObject.FindGameObjectWithTag("EventBoard").transform, false);
                        newChoice2.transform.SetParent(GameObject.FindGameObjectWithTag("EventBoard").transform, false);
                        newChoice3.transform.SetParent(GameObject.FindGameObjectWithTag("EventBoard").transform, false);

                        newChoice1.transform.localPosition = new Vector3(0, -100, 0);
                        newChoice2.transform.localPosition = new Vector3(175, -100, 0);
                        newChoice3.transform.localPosition = new Vector3(350, -100, 0);
                    }

                    GameLoop.gameLoop.eventPhase = true;
                    GameLoop.gameLoop.isEvent = true;
                }
                else if (lines[j][0] == '5')
                {
                    DiceBoardManager.diceBoardManager.dice += Convert.ToInt32(sections[1]);
                }
                else if (lines[j][0] == '6')
                {
                    GameLoop.gameLoop.victoryText = sections[0].Substring(1, sections[0].Length - 1);
                    GameLoop.gameLoop.defeatText = sections[1];
                }
                else if (lines[j][0] == '7')
                {
                    if (sections[0].Substring(1, sections[0].Length - 1) == "inventory")
                    {
                        MainChar.mainChar.Inventory.Add(sections[1], Convert.ToInt32(sections[2]));
                    }
                }
            }
            dialogues.Add(tempPart);
        }
        DialogueManager.dialogueManager.dialogues = dialogues;
    }
}

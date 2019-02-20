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

    int count;

    public void ReadText(TextAsset textFile)
    {
        parts = textFile.text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < parts.Length; i++)
        {
            DialoguePart tempPart = new DialoguePart();

            lines = parts[i].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < lines.Length; j++)
            {
                if (lines[j][0] == '1')
                {
                    tempPart.dialogName = lines[j].Substring(1, lines[j].Length - 1);
                    //dialogues[count, 0] = lines[j].Substring(1, lines[j].Length - 1);
                }
                else if (lines[j][0] == '2')
                {
                    tempPart.dialogSentence += "_" + lines[j].Substring(1, lines[j].Length - 1);
                    //dialogues[count, 1] += "_" + lines[j].Substring(1, lines[j].Length - 1);
                }
            }
            dialogues.Add(tempPart);
            count++;
        }
        DialogueManager.dialogueManager.dialogues = dialogues;
    }
}

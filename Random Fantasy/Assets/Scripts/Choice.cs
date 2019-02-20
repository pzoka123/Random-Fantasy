﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    string choiceName;
    public string ChoiceName { get => choiceName; set => choiceName = value; }

    string[] sentences;
    public string[] Sentences { get => sentences; set => sentences = value; }
    
    public void Clicked()
    {
        gameObject.GetComponent<Animator>().SetBool("isActive", true);
        GameLoop.gameLoop.textFile = Resources.Load("Texts/Sleep") as TextAsset;
        GameLoop.gameLoop.dialogueStart = true;
    }
}
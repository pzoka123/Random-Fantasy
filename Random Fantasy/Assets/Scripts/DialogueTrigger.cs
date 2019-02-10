using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isClicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TriggerDialogue()
    {
        isClicked = true;
        GameObject.FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    GameObject[] choices;

    // Start is called before the first frame update
    void Start()
    {
        choices = GameObject.FindGameObjectsWithTag("ChoiceCard");
    }

    public void Clicked()
    {
        gameObject.GetComponent<Animator>().SetBool("isActive", true);
        EventManager.eventManager.currClicked = gameObject;
        EventManager.eventManager.HideChoice();
        GameLoop.gameLoop.currentAction = GameLoop.Actions.eventAction;
        GameLoop.gameLoop.nextAction = GameLoop.Actions.eventAction;
    }
}

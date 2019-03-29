using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager eventManager { get; set; }

    public GameObject currClicked;

    GameObject eventBoard;
    GameObject eventCard;
    GameObject[] choiceCards;

    void Awake()
    {
        if (eventManager == null)
        {
            eventManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        eventBoard = GameObject.FindGameObjectWithTag("EventBoard");
        eventCard = GameObject.FindGameObjectWithTag("EventCard");
        choiceCards = GameObject.FindGameObjectsWithTag("ChoiceCard");
    }

    public void Display()
    {
        GameObject.FindGameObjectWithTag("EventBoard").GetComponent<Animator>().SetBool("isActive", true);
    }

    public void Hide()
    {
        eventBoard.GetComponent<Animator>().SetBool("isActive", false);
    }

    public void ReturnEvent()
    {
        eventCard = GameObject.FindGameObjectWithTag("EventCard");
        eventCard.GetComponent<Animator>().SetBool("isActive", false);
    }

    public void HideEvent()
    {
        eventCard = GameObject.FindGameObjectWithTag("EventCard");
        eventCard.GetComponent<Image>().enabled = false;
        eventCard.transform.GetChild(0).GetComponent<Image>().enabled = false;
        eventCard.GetComponentInChildren<Text>().enabled = false;
    }

    public void ShowChoice()
    {
        choiceCards = GameObject.FindGameObjectsWithTag("ChoiceCard");
        foreach (GameObject card in choiceCards)
        {
            card.GetComponent<Image>().enabled = true;
            card.GetComponent<Button>().enabled = true;
            card.GetComponentInChildren<Text>().enabled = true;
        }
    }

    public void ReturnChoice()
    {
        currClicked.GetComponent<Animator>().SetBool("isActive", false);
        currClicked.GetComponent<Image>().enabled = false;
        currClicked.GetComponent<Button>().enabled = false;
        currClicked.GetComponentInChildren<Text>().enabled = false;

        Destroy(eventCard);
        foreach (GameObject card in choiceCards)
        {
            Destroy(card);
        }
    }

    public void HideChoice()
    {
        choiceCards = GameObject.FindGameObjectsWithTag("ChoiceCard");
        foreach (GameObject card in choiceCards)
        {
            if (card != currClicked)
            {
                card.GetComponent<Image>().enabled = false;
                card.GetComponent<Button>().enabled = false;
                card.GetComponentInChildren<Text>().enabled = false;
            }
        }
    }
}

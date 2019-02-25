using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public GameObject currClicked;
    public static EventManager eventManager { get; set; }

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
        //eventCard.SetActive(true);
        //foreach (GameObject card in choiceCards)
        //{
        //    card.SetActive(true);
        //}
        GameObject.FindGameObjectWithTag("EventBoard").GetComponent<Animator>().SetBool("isActive", true);
    }

    public void Hide()
    {
        //eventCard.SetActive(false);
        //foreach (GameObject card in choiceCards)
        //{
        //    card.SetActive(false);
        //}

        eventBoard.GetComponent<Animator>().SetBool("isActive", false);
    }

    public void ReturnEvent()
    {
        eventCard.GetComponent<Animator>().SetBool("isActive", false);
    }

    public void HideEvent()
    {
        eventCard.GetComponent<Image>().enabled = false;
        eventCard.transform.GetChild(0).GetComponent<Image>().enabled = false;
        eventCard.GetComponentInChildren<Text>().enabled = false;
    }

    public void ShowChoice()
    {
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
    }

    public void HideChoice()
    {
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

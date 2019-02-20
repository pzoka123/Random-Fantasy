using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager eventManager { get; set; }

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

    public void Display()
    {
        GameObject.FindGameObjectWithTag("EventCard").SetActive(true);
        foreach (GameObject card in GameObject.FindGameObjectsWithTag("ChoiceCard"))
        {
            card.SetActive(true);
        }
    }

    public void Hide()
    {
        GameObject.FindGameObjectWithTag("EventCard").SetActive(false);
        foreach (GameObject card in GameObject.FindGameObjectsWithTag("ChoiceCard"))
        {
            card.SetActive(false);
        }
    }

    public void HideEvent()
    {
        GameObject.FindGameObjectWithTag("EventCard").GetComponent<Animator>().SetBool("isActive", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceData : MonoBehaviour
{
    public List<Sprite> faces = new List<Sprite>(6);
    
    void Start()
    {
        foreach (Sprite symbol in DiceBoardManager.diceBoardManager.diceSprites)
        {
            if (symbol.name == "SwordDice")
            {
                faces.Add(symbol);
                faces.Add(symbol);
            }
            else if (symbol.name == "ShieldDice")
            {
                faces.Add(symbol);
            }
            faces.Add(symbol);
        }
    }

    public void RollResult()
    {
        int rnd = Random.Range(0, 6);
        gameObject.GetComponent<Image>().overrideSprite = faces[rnd];
        gameObject.GetComponent<Animator>().SetBool("roll", false);
        DiceBoardManager.diceBoardManager.rolled = true;
    }
}

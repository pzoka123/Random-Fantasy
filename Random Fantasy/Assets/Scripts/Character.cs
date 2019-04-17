using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool attack = false;
    public bool defend = false;
    public bool magic = false;
    public bool die = false;

    Animator anim;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void CharAttack()
    {
        anim.SetBool("attack", true);
        attack = true;
    }

    public void CharDefend()
    {
        Debug.Log("Defend!");
        defend = true;
    }

    public void CharMagic()
    {
        Debug.Log("MAGIC!");
        magic = true;
    }

    public void CharDie()
    {
        anim.SetBool("die", true);
        die = true;
    }

    public void EndAttack()
    {
        anim.SetBool("attack", false);
    }
}

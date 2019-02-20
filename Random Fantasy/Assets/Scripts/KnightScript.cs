using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{
    DiceScript diceManager;

    Animator anim;
    Vector3 startPos;
    Vector3 endPos;

    float speed = 1.5f;
    float lerpValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        diceManager = GameObject.FindObjectOfType<DiceScript>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (diceManager.canAtk == true)
        {
            anim.SetBool("run", true);
            lerpValue += Time.deltaTime / speed;
            endPos = new Vector3(5.25f, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(startPos, endPos, lerpValue);
            if (transform.position == endPos)
            {
                anim.SetBool("run", false);
                anim.SetBool("attack", true);
                diceManager.canAtk = false;
            }
        }
        if (diceManager.canDie2)
        {
            anim.SetBool("die", true);
            diceManager.endFight = 2;
        }
    }

    public void EndAttack()
    {
        anim.SetBool("attack", false);
        diceManager.canDie = true;
    }
}

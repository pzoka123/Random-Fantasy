using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight2Script : MonoBehaviour
{
    Master gameManager;
    DiceScript diceManager;

    Animator anim;
    Vector3 startPos;
    Vector3 endPos;

    float speed = 3.0f;
    float lerpValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        gameManager = GameObject.FindObjectOfType<Master>();
        diceManager = GameObject.FindObjectOfType<DiceScript>();
        startPos = transform.position;
        endPos = new Vector3(6.0f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.fight == true)
        {
            anim.SetBool("isWalking", true);
            lerpValue += Time.deltaTime / speed;
            transform.position = Vector3.Lerp(startPos, endPos, lerpValue);
            if (transform.position == endPos)
            {
                anim.SetBool("isWalking", false);
                gameManager.fight = false;
                lerpValue = 0;
                startPos = transform.position;
            }
        }
        if (diceManager.canDie)
        {
            anim.SetBool("die", true);
            gameManager.DiceBoardHide();
            diceManager.endFight = 1;
        }
        if (diceManager.canAtk2)
        {
            anim.SetBool("run", true);
            lerpValue += Time.deltaTime / speed;
            endPos = new Vector3(-4.5f, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(startPos, endPos, lerpValue);
            if (transform.position == endPos)
            {
                anim.SetBool("run", false);
                anim.SetBool("attack", true);
                diceManager.canAtk2 = false;
            }
        }
    }

    public void EndAttack()
    {
        anim.SetBool("attack", false);
        diceManager.canDie2 = true;
    }
}

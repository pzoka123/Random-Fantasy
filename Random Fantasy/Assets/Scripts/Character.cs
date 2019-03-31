using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool move;

    public bool attack;
    public bool die;
    public bool isDead;

    Vector3[] positions = {
        new Vector3(-12,0,0),
        new Vector3(-6,0,0),
        new Vector3(-3,0,0),
        new Vector3(0,0,0),
        new Vector3(3,0,0),
        new Vector3(6,0,0),
        new Vector3(12,0,0)
    };
    public int posNum;

    Animator anim;
    Vector3 startPos;
    Vector3 endPos;
    
    float speed = 3.0f;
    float lerpValue = 0;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        startPos = transform.position;
        endPos = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharMove()
    {
        if (move)
        {
            endPos = new Vector3(positions[posNum].x, transform.position.y, transform.position.z);
            lerpValue += Time.deltaTime / speed;
            transform.position = Vector3.Lerp(startPos, endPos, lerpValue);
            if (transform.position == endPos)
            {
                anim.SetBool("walk", false);
                anim.SetBool("run", false);
                move = false;
                lerpValue = 0;
                startPos = transform.position;
            }
        }
    }

    public void CharAttack(GameObject target)
    {
        if (attack)
        {
            endPos = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            anim.SetBool("run", true);
            lerpValue += Time.deltaTime / speed;
            transform.position = Vector3.Lerp(startPos, endPos, lerpValue);
            if (transform.position == endPos)
            {
                anim.SetBool("run", false);
                anim.SetBool("attack", true);
                attack = false;
                lerpValue = 0;
                startPos = transform.position;
            }
        }
    }

    public void CharDie()
    {
        if (die)
        {
            isDead = true;
            anim.SetBool("die", true);
            die = false;
        }
        if (gameObject.tag == "Player")
        {
            GameLoop.gameLoop.isEnd = true;
        }
    }

    public void EndAttack()
    {
        if (gameObject.tag == "Player" && anim.GetBool("attack"))
        {
        }
        else
        {
        }
        anim.SetBool("attack", false);
    }
}

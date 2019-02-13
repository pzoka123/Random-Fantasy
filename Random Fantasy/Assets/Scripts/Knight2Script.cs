using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight2Script : MonoBehaviour
{
    Master gameManager;

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
        startPos = transform.position;
        endPos = new Vector3(6.0f, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.fight == true)
        {
            anim.SetBool("isWalking", true);
            lerpValue += Time.deltaTime / speed;
            transform.position = Vector2.Lerp(startPos, endPos, lerpValue);
            if (transform.position == endPos)
            {
                anim.SetBool("isWalking", false);
                gameManager.fight = false;
            }
        }
    }
}

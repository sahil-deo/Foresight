using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalScript : MonoBehaviour
{
    private Animator anim;
    private gameControllerScript gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<gameControllerScript>();
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gc.source.clip = gc.goal;
            gc.source.Play();

            //Debug.Log("Collided with player");
            //anim.Play("GoalReached");
        }
    }
}

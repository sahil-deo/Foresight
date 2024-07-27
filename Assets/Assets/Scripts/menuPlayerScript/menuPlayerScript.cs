using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuPlayerScript : MonoBehaviour
{

    public Transform[] transforms;

    public float speed;
    
    Vector3 Destination;

    Animator anim;

    int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        anim = GetComponent<Animator>();
        anim.Play("move");
        Destination = transforms[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Destination == transform.position)
        {
            Destination = transforms[index].position;
            index++;
            if(index == transforms.Length)
            {
                index = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, Destination, speed*Time.deltaTime);

    }
}

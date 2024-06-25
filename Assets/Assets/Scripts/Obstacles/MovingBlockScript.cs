using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockScript : MonoBehaviour
{
    public Transform pos1, pos2;
    public GameObject block;
    public float speed;


    Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        block.transform.position = pos1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(block.transform.position == pos1.position)
        {
            destination = pos2.position;
        }else if(block.transform.position == pos2.position)
        {
            destination = pos1.position;
        }

        block.transform.position = Vector3.MoveTowards(block.transform.position, destination, speed * Time.deltaTime);
    }
}

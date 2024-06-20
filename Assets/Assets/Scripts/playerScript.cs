using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public float speed;

    public Transform _start;



    bool started, reached;


    gameControllerScript gc;

    List<int> moves = new List<int>();

    Vector3 Destination;



    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<gameControllerScript>();
        started = false;
        Destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GoToDestination();
        if (Input.GetKeyDown(KeyCode.Space) && !started)
        {
            started = true;
            StartCoroutine(Execute());
        }
    }
    void Move()
    {

        if (started) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            gc.spawnIndicator(gc.iU);
            moves.Add(1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            gc.spawnIndicator(gc.iL);

            moves.Add(2);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            gc.spawnIndicator(gc.iD);
            moves.Add(3);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            gc.spawnIndicator(gc.iR);
            moves.Add(4);
        }
    }

    void GoToDestination()
    {
        if (!started) return;

        if (Vector2.Distance(Destination, transform.position) != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, speed * Time.deltaTime);
        }
    }

    IEnumerator Execute()
    {

        for (int i = 0; i < moves.Count; i++)
        {
            if (!started) break;

            int move = moves[i];

            while (Vector2.Distance(transform.position, Destination) != 0)
            {
                yield return null;
            }


            switch (move)
            {
                case 1:

                    Destination = transform.position + new Vector3(0, 1, 0);
                    break;
                case 2:
                    Destination = transform.position + new Vector3(-1, 0, 0);
                    break;
                case 3:
                    Destination = transform.position + new Vector3(0, -1, 0);
                    break;
                case 4:
                    Destination = transform.position + new Vector3(1, 0, 0);
                    break;
            }

            yield return new WaitForSeconds(0.1f);

        }

        if (!reached)
        {

            Invoke("Restart", 1f);
        }
        else
        {
            Invoke("Restart", 1f);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Restart();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Win"))
        {
            reached = true;
        }
    }


    void Restart()
    {
        transform.position = _start.position;
        started = false;
        Destination = transform.position;
        moves.Clear();
        reached = false;
        gc.clearIndicators();
    }
}

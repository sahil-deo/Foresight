using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialPlayerController : MonoBehaviour
{
    public float speed;

    public Transform _start;



    public bool started;

    bool reached;


    tutorialGameController gc;
    tutorialScript ts;

    List<int> moves = new List<int>();

    Vector3 Destination;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("TutorialController").GetComponent<tutorialGameController>();
        ts = GameObject.Find("TutorialController").GetComponent<tutorialScript>();
        anim = GetComponent<Animator>();    

        started = false;
        Destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GoToDestination();
        if (tutorialGameController.levelStarted && !started)
        {
            started = true;
            StartCoroutine(Execute());
        }
    }

    void Move()
    {
        if (started) return;

        if (gameControllerScript.isIndicatorsHidden && (
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D)))
        {
            gc.clearIndicators();
            moves.Clear();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            
            if (ts.checkPoint == 0) ts.checkPoint = 1;
            gc.spawnIndicator(gc.iU);
            moves.Add(1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (ts.checkPoint == 0) ts.checkPoint = 1;
            gc.spawnIndicator(gc.iL);
            moves.Add(2);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (ts.checkPoint == 0) ts.checkPoint = 1;
            gc.spawnIndicator(gc.iD);
            moves.Add(3);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (ts.checkPoint == 0) ts.checkPoint = 1;
            gc.spawnIndicator(gc.iR);
            moves.Add(4);
        }



        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearMoves();
            if (ts.checkPoint == 3) ts.checkPoint = 4;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Go Back a move
            BackSpaceMove();
            if (ts.checkPoint == 4) ts.checkPoint = 5;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Start
            gc.startLevelButton();
            if (ts.checkPoint == 1) ts.checkPoint = 2;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gc.reuseMovesButton();
            if (ts.checkPoint == 2) ts.checkPoint = 3;
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
        Debug.Log("Executing");
        for (int i = 0; i < moves.Count; i++)
        {
            if (!started) break;

            int move = moves[i];

            anim.Play("move");

            while (Vector2.Distance(transform.position, Destination) != 0)
            {
                yield return null;
            }


            anim.Play("stop");
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
        if (other.gameObject.CompareTag("Goal"))
        {
            reached = true;
        }
    }


    void Restart()
    {
        transform.position = _start.position;
        started = false;
        tutorialGameController.levelStarted = false;
        Destination = transform.position;
        reached = false;
        gc.hideIndicators();
    }

    public void ClearMoves()
    {
        gc.source.clip = gc.click2;
        gc.source.Play();
        if (ts.checkPoint == 3) ts.checkPoint = 4;


        gc.clearIndicators();

        moves.Clear();
    }

    public void BackSpaceMove()
    {
        if (ts.checkPoint == 4) ts.checkPoint = 5;

        if (gc.indicators.Count == 0) return;

        gc.source.clip = gc.click2;
        gc.source.Play();

        moves.RemoveAt(moves.Count - 1);
        gc.backSpaceIndicator();

    }


}

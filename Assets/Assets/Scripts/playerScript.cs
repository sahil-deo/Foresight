using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    public float speed;

    public Transform _start;

    private Animator anim;


    public bool started;

    bool reached;


    gameControllerScript gc;

    List<int> moves = new List<int>();

    Vector3 Destination;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
        gc = GameObject.Find("GameController").GetComponent<gameControllerScript>();
        started = false;
        Destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameControllerScript.isMenuOpen && Input.GetKeyDown(KeyCode.Escape)) gc.closeMenuButton();
        else if (!gameControllerScript.isMenuOpen && Input.GetKeyDown(KeyCode.Escape)) gc.menuButton();

        if (gameControllerScript.isMenuOpen) return;

        Move();
        GoToDestination();
        if (gameControllerScript.levelStarted && !started)
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


        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearMoves();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Go Back a move
            BackSpaceMove();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Start
            gc.startLevelButton();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gc.reuseMovesButton();
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
            if (!started)
            {
                anim.Play("stop");
                break;
            }

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
            Invoke("NextLevel", 1f);
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
        gameControllerScript.levelStarted = false;
        Destination = transform.position;
        reached = false;
        gc.hideIndicators();
    }

    void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings-1))
        {
            SceneManager.LoadScene(0);
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClearMoves()
    {
        gc.source.clip = gc.click2;
        gc.source.Play();

        
        gc.clearIndicators();

        moves.Clear();
    }

    public void BackSpaceMove()
    {

        if (gc.indicators.Count == 0) return;

        gc.source.clip = gc.click2;
        gc.source.Play();

        moves.RemoveAt(moves.Count - 1);
        gc.backSpaceIndicator();

    }

    
}

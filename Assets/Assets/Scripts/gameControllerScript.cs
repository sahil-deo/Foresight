using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.UI;

public class gameControllerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject iL, iR, iU, iD;
    
    public Transform indicatorPos;

    public AudioClip
        click,
        click2,
        goal;

    public AudioSource source;
    

    public static int iCount;
    
    public static bool 
        levelStarted,
        isIndicatorsHidden;


    public GameObject startButton;

    public List<GameObject> indicators = new List<GameObject>();


    
    private GameObject newIndicator;

    void Start()
    {
        iCount = 0;
        
        levelStarted = false;
        isIndicatorsHidden = false;

        startButton.GetComponent<Button>().enabled = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelStarted)
        {
            startButton.GetComponent<Button>().enabled = true;
        }
    }

    public void spawnIndicator(GameObject _indicator)
    {
        source.clip = click;
        source.Play();

        newIndicator = Instantiate(_indicator, indicatorPos.position, quaternion.identity);
        iCount++;

        foreach (GameObject indicator in indicators)
        {
            indicator.transform.position = new Vector3(indicator.transform.position.x + 1.2f, indicator.transform.position.y, indicator.transform.position.z);
        }

        indicators.Add(newIndicator);

    }


    public void clearIndicators()
    {
        isIndicatorsHidden = false;
        foreach (GameObject indicator in indicators)
        {
            Destroy(indicator);
        }
        indicators.Clear();
    }

    [HideInInspector]
    public void hideIndicators()
    {
        isIndicatorsHidden = true;
        
        foreach (GameObject indicator in indicators)
        {
             indicator.gameObject.SetActive(false);
        }
    }

    //Button Functions

    public void startLevelButton()
    {
        if (isIndicatorsHidden)
        {
            GameObject.Find("Player").GetComponent<playerScript>().ClearMoves();
        }

        levelStarted = true;

        source.clip = click2;
        source.Play();

        startButton.GetComponent<Button>().enabled = false;
    }

    public void reuseMovesButton()
    {
        if (!isIndicatorsHidden) return;
        if (levelStarted) return;

        isIndicatorsHidden = false;

        source.clip = click2;
        source.Play();

        foreach (GameObject indicator in indicators)
        {
            indicator.gameObject.SetActive(true);
        }
    }

    public void clearMoves()
    {
        GameObject.Find("Player").GetComponent<playerScript>().ClearMoves();
    }

    public void backSpaceMove()
    {
        GameObject.Find("Player").GetComponent<playerScript>().BackSpaceMove();
    }


    //Helper Function
    [HideInInspector]
    public void backSpaceIndicator()
    {
        if (indicators.Count == 0) return; 

        iCount--;

        Destroy(indicators[indicators.Count - 1]);
        indicators.RemoveAt(indicators.Count - 1);
        
        foreach (GameObject indicator in indicators)
        {
            indicator.transform.position = new Vector3(indicator.transform.position.x - 1.2f, indicator.transform.position.y, indicator.transform.position.z);
        }



    }
}

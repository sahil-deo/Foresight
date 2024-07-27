using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialScript : MonoBehaviour
{
    public GameObject
        moveText,
        startText,
        reuseText,
        clearText,
        backSpaceText,
        done;
    public float checkPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (checkPoint)
        {
            case 0:
                moveText.SetActive(true);
                break;
            case 1:
                moveText.SetActive(false);
                startText.SetActive(true);
                break;
            case 2:
                reuseText.SetActive(true);
                moveText.SetActive(false);
                startText.SetActive(false);
                break;
            case 3:
                clearText.SetActive(true);
                reuseText.SetActive(false);
                moveText.SetActive(false);
                startText.SetActive(false);
                break;
            case 4:
                backSpaceText.SetActive(true);
                clearText.SetActive(false);
                reuseText.SetActive(false);
                moveText.SetActive(false);
                startText.SetActive(false);
                break;
            case 5:
                done.SetActive(true);
                backSpaceText.SetActive(false);
                clearText.SetActive(false);
                reuseText.SetActive(false);
                moveText.SetActive(false);
                startText.SetActive(false);

                Invoke("NextLevel", 2f);
                
                break;
        }
    }


    void NextLevel()
    {

        if (SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings - 1))
        {
            SceneManager.LoadScene(0);
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

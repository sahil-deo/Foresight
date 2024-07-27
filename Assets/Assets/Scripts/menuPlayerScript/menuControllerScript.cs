using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControllerScript : MonoBehaviour
{
    static bool firstTime = true;
    // Start is called before the first frame update
    public void Player()
    {
        if (firstTime)
        {
            firstTime = false;  
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;

public class gameControllerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject iL, iR, iU, iD;
    public Transform indicatorPos;

    public static int iCount;

    public List<GameObject> indicators = new List<GameObject>();
    private GameObject newIndicator;

    void Start()
    {
        iCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnIndicator(GameObject _indicator)
    {
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
        foreach (GameObject indicator in indicators)
        {
            Destroy(indicator);
        }
        indicators.Clear();
    }

}

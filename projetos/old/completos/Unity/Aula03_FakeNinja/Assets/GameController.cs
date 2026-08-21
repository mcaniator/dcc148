using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject deathPrefab;
    public GameObject nicePrefab;

    private GameObject activeCube;
    private float elapsedTime = 0;
    private int points = 0;
    private float maxElapsed = 5;

    private GameObject deathInstance;
    private GameObject niceInstance;

    // Start is called before the first frame update
    void Start()
    {
        deathInstance = Instantiate(deathPrefab);
        niceInstance = Instantiate(nicePrefab);
        deathInstance.SetActive(false);
        activeCube = nicePrefab;
    }

    void NewCube()
    {
        elapsedTime = 0;

        float randVal = Random.Range(0.0f, 1.0f);
        if(randVal > 0.8)
        {
            activeCube = deathInstance;
            niceInstance.SetActive(false);
            deathInstance.SetActive(true);
        }
        else
        {
            activeCube = niceInstance;
            niceInstance.SetActive(true);
            deathInstance.SetActive(false);
        }

        float randX = Random.Range(-6.0f, 6.0f);
        float randY = Random.Range(-3.0f, 3.0f);
        activeCube.transform.position = new Vector3(randX, randY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= maxElapsed)
        {
            points -= 2;
            NewCube();
        }

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if(activeCube.tag == "Death")
            {
                Debug.Log("Points: " + points);
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                points++;

                if(points % 10 == 0)
                    maxElapsed /= 2;
            }

            NewCube();
        }
    }
}

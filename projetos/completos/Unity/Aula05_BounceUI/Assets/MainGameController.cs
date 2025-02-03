using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Bounce");   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    float tempoDecorrido;

    public Light luz;

    private MainGameController mainGame;
    private AudioSource musica;
    
    // Start is called before the first frame update
    void Start()
    {
        tempoDecorrido = 0;
        mainGame = GameObject.Find("MainGameController").GetComponent<MainGameController>();
        musica = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        tempoDecorrido += Time.deltaTime;
        if(tempoDecorrido > 0.5)
        {
            luz.enabled = true;

            int x = Random.Range(0, 2);
            int y = Random.Range(0, 2);

            if(x == 0) x = -1;
            if(y == 0) y = -1;

            luz.transform.position = new Vector3(x, y, luz.transform.position.z);
                
            tempoDecorrido = 0;

        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            musica.Stop();
            mainGame.IniciarJogo();
            SceneManager.UnloadSceneAsync("Intro");
        }
    }
}

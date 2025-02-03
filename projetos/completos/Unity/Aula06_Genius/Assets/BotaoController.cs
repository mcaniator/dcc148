using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoController : MonoBehaviour
{
    private AudioSource audioClip;
    private Light luzPontual;
    private float tempoDecorrido = 0;
    private bool botaoPressionado = false;
    
    // Start is called before the first frame update
    void Start()
    {
        audioClip = GetComponent<AudioSource>();   
        
        GameObject luzObj = GameObject.Find("LuzPontual");
        luzPontual = luzObj.GetComponent<Light>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(botaoPressionado)
        {
            tempoDecorrido += Time.deltaTime;
            if(tempoDecorrido >= 1)
            {
                luzPontual.enabled = false;
                tempoDecorrido = 0;
                botaoPressionado = false;
            }
        }   
    }

    public void PressionaBotao()
    {
        audioClip.Play();

        Vector3 botaoPos = transform.position;
        Vector3 luzPos = luzPontual.transform.position;

        // a coordenada z não será alterada, para que a luz fique na frente do cubo
        luzPos.x = botaoPos.x;
        luzPos.y = botaoPos.y;
        luzPontual.transform.position = luzPos;

        luzPontual.enabled = true;
        botaoPressionado = true;
    }
}

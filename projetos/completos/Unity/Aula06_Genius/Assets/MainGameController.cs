using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public BotaoController[] botoes;
    public GameObject player;
    public Light luz;

    private List<int> sequencia;
    private int tamanhoSequencia;
    private const int TAMANHO_MINIMO = 4;
    private float tempoDecorrido;
    private int indiceSequencia;

    private bool animacaoInicial;
    private bool jogando;
    private int botaoSelecionado = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        animacaoInicial = true;

        sequencia = new List<int>();
        tamanhoSequencia = TAMANHO_MINIMO;

        jogando = false;
        GeraSequencia();
    }

    void GeraSequencia()
    {
        sequencia.Clear();
        for(int i = 0; i < tamanhoSequencia; i++)
        {
            int val = Random.Range(0, 4); // 4 botões possíveis
            sequencia.Add(val);
        }
    }

    void TocaSequencia()
    {
        if(indiceSequencia < tamanhoSequencia)
        {
            if(tempoDecorrido < 1)
                tempoDecorrido += Time.deltaTime;
            else
            {
                botoes[sequencia[indiceSequencia]].PressionaBotao();
                indiceSequencia++;
                tempoDecorrido = 0;
            }
        }
        else
        {
            indiceSequencia = 0;
            jogando = true;
        }
    }

    void GameOver(bool vitoria)
    {
        if(vitoria)
        {
            Debug.Log("Venceu");
            tamanhoSequencia++;
        }
        else
        {
            Debug.Log("Perdeu");
            tamanhoSequencia = TAMANHO_MINIMO;
        }
        NovoJogo();
    }

    void NovoJogo()
    {
        GeraSequencia();
        jogando = false;
        indiceSequencia = 0;
    }

    void ExecutaRodada()
    {
        Vector3 pos = player.transform.position;
        
        if(pos.x < 0 && pos.y > 0)
            botaoSelecionado = 0;
        else if(pos.x > 0 && pos.y > 0)
            botaoSelecionado = 1;
        else if(pos.x < 0 && pos.y < 0)
            botaoSelecionado = 2;
        else
            botaoSelecionado = 3;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(indiceSequencia < tamanhoSequencia)
            {
                if(sequencia[indiceSequencia] == botaoSelecionado)
                {
                    botoes[botaoSelecionado].PressionaBotao();
                    indiceSequencia++;
                }
                else
                    GameOver(false);
            }

            if(indiceSequencia == tamanhoSequencia)
                GameOver(true);
        }
    }

    public void IniciarJogo()
    {
        animacaoInicial = false;
        player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(animacaoInicial == false)
        {
            if(jogando)
                ExecutaRodada();
            else
                TocaSequencia();
        }
    }
}

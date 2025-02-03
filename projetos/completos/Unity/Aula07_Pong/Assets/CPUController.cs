using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUController : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private PlayerController player;

    private float speed;
    private float ballXPrev;

    void Start()
    {
        ballXPrev = ball.transform.position.x;

        // Aqui a velocidade da CPU foi simplesmente definida como a mesma do jogador. Uma forma simples de 
        // alterar a dificuldade é torná-la mais lenta ou mais rápida, proporcionalmente à velocidade do
        // jogador. Outras lógicas também podem ser implementadas, como, por exemplo, introduzir um atraso
        // na reação.
        speed = player.Speed;
    }

    void FixedUpdate()
    {
        // Só se move se a bola estiver vindo na sua direção
        if(ball.transform.position.x - ballXPrev > 0)
        {
            float dy = ball.transform.position.y - transform.position.y;
            float dir = (dy < 0) ? -1 : 1;
            transform.Translate(0, dir * Time.fixedDeltaTime * speed, 0);
        }

        ballXPrev = ball.transform.position.x;
    }
}

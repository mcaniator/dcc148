using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerController player;
    public float speed;
    public float minDist;

    private Vector2 direction;
    private Vector2 destino;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(-1, 0);
        destino = new Vector2(transform.position.x, 4);
    }

    // Update is called once per frame
    void Update()
    {
        // Se distância menor que limiar, inimigo entra no modo de "alerta"
        if(Vector2.Distance(player.transform.position, transform.position) < minDist)
        {
            // Move na direção do jogador
            Vector2 moveDir = player.transform.position - transform.position;
            moveDir.Normalize();

            // Ajusta o vetor que indica para que lado o inimigo está apontando 
            // (se moveDir.x < 0, quer dizer que o jogador está à esquerda do inimigo)
            direction.x = (moveDir.x < 0) ? -1 : 1;

            // Determina se ambos olham na mesma direção
            // Obs.: neste exercício o objetivo era praticar o Vector2.Dot, mas, nesse caso, poderíamos também
            // simplesmente verificar se o produto das coordenadas x é > 0
            // if(player.Direction.x * moveDir.x > 0)
            if(Vector2.Dot(player.Direction, direction) == 1)
            {
                // Desloca o inimigo em relação ao sistema de coordenadas global (pois o objeto foi rotacionado no editor)
                transform.Translate(Time.deltaTime * speed * moveDir, Space.World);
            }
        }
        else
        {
            // Se jogador distante, inimigo move no padrão vertical
            transform.position = Vector2.MoveTowards(transform.position, destino, Time.deltaTime * speed);
            if(Mathf.Abs(transform.position.y - destino.y) < 0.1)
                destino.y *= -1;
        }
    }
}

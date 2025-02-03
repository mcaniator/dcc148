using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesPrefabs;
    [SerializeField] private int[] enemiesPerSection;

    private ObjectPool[] enemiesPools;
    private GameObject cannon;
    private float tempoDecorrido;
    private float spawnRate; // quantos segundos para gerar o próximo inimigo
    private int enemyCount;
    private int section;
    private int level;
    private bool boss;

    // Start is called before the first frame update
    void Start()
    {
        enemiesPools = new ObjectPool[3];
        enemiesPools[0] = new ObjectPool(enemiesPrefabs[0], 10);
        enemiesPools[1] = new ObjectPool(enemiesPrefabs[1], 10);
        enemiesPools[2] = new ObjectPool(enemiesPrefabs[2], 10);
        cannon = Instantiate(enemiesPrefabs[3]);
        tempoDecorrido = 0;
        spawnRate = 2;
        enemyCount = 0;
        section = 0;
        level = 0;
        boss = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!boss)
        {
            tempoDecorrido += Time.deltaTime;
            if(tempoDecorrido >= spawnRate)
            {
                GameObject enemy = enemiesPools[section].GetFromPool();
                if(enemy)
                {
                    enemy.transform.position = new Vector2(9, Random.Range(-4.0f, 4.0f));
                    tempoDecorrido = 0;
                    enemyCount++;
                }

                if(enemyCount % enemiesPerSection[section] == 0)
                {
                    section++;
                    if(section == 3)
                    {
                        if(level == 3)
                        {
                            boss = true;
                        }
                        else
                        {
                            section = 0;
                            spawnRate /= 2;
                            cannon.SetActive(true);
                            level++;
                        }
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyController : MonoBehaviour
{
    private Transform player;
    private Transform cannonBase;
    private Transform cannon;
    private GameObject bullet;

    private Vector3 pivotToRotatedObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        cannonBase = transform.GetChild(0);
        cannon = transform.GetChild(1);
        bullet = transform.GetChild(2).gameObject;
        // cannon = GetComponentsInChildren<Transform>()[2]; // nesta função, o índice 0 contém o pai
        
        pivotToRotatedObject = cannon.position - cannonBase.position;
        bullet.SetActive(false);
    }

    void Shoot()
    {
        if(!bullet.activeSelf)
        {
            bullet.SetActive(true);
            bullet.transform.position = cannon.position;
            bullet.transform.localRotation = cannon.localRotation;
            bullet.transform.Rotate(0, 0, 90);
            bullet.tag = "Enemy";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            Quaternion rot = Quaternion.FromToRotation(cannon.up, direction);

            //=== OPÇÃO 1 ===//
            
            // Executa as 3 operações indicadas nos slides

            cannon.localRotation *= rot;
            pivotToRotatedObject = rot * pivotToRotatedObject;
            cannon.position = cannonBase.position + pivotToRotatedObject;


            //======= OUTRAS SOLUÇÕES =========//

            //=== OPÇÃO 2 ===//
            
            // Nesse caso, não atualizamos pivotToRotatedObject, pois usamos a translação segundo
            // o sistema de coordenadas local do objeto (Space.Self)

            // cannon.Translate(-pivotToRotatedObject);
            // cannon.localRotation *= rot;
            // cannon.Translate(pivotToRotatedObject);

            //=== OPÇÃO 3 ===//
            
            // Solução sem quatérnios

            // float angle = Vector2.SignedAngle(cannon.up, direction);
            // Debug.Log(angle);
            // cannon.RotateAround(cannonBase.position, Vector3.forward, angle);

            Shoot();
        }
    }
}

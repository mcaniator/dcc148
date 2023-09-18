using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyController : MonoBehaviour
{
    private Transform player;
    private Transform cannonBase;
    private Transform cannon;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        cannonBase = transform.GetChild(0);
        cannon = transform.GetChild(1);
        // cannon = GetComponentsInChildren<Transform>()[2]; // nesta função, o índice 0 contém o pai
        Debug.Log(cannon);
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            Quaternion rot = Quaternion.FromToRotation(cannon.up, direction);
            // Quaternion rot = Quaternion.LookRotation(direction, Vector3.back);
            
            Vector3 toPivot = new Vector3(0, 0.66f, 0);

            Debug.Log(cannon.localPosition);

            cannon.Translate(-toPivot);
            cannon.localRotation *= rot;
            cannon.Translate(toPivot);

            // transform.localRotation *= rot;
            // cannonBase.localRotation *= Quaternion.Inverse(rot);

            // Vector3 toPivot = cannon.position - cannonBase.position;
            // cannon.localRotation *= rot;
            // toPivot = rot * toPivot;
            // cannon.position = cannonBase.position + toPivot;

            // cannon.RotateAround(cannonBase.position, Vector3.forward, Input.GetAxis("Horizontal"));
        }
    }
}

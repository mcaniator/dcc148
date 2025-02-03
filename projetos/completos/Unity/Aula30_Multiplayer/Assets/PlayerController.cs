using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    public NetworkVariable<Vector3> serverPosition = new NetworkVariable<Vector3>();

    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        {
            if(NetworkManager.Singleton.IsServer)
            {
                transform.position = new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3));
                serverPosition.Value = transform.position;
                Debug.Log("Owner: " + transform.position);
            }
            else
                ChangePositionServerRpc(new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3)));
        }
    }

    [ServerRpc]
    void ChangePositionServerRpc(Vector3 position)
    {
        Debug.Log("Server: " + position);
        serverPosition.Value = position;
    }

    void Update()
    {
        // transform.position = serverPosition.Value;
        // if(IsOwner)
        // {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            float dx = Input.GetAxis("Horizontal");
            float dy = Input.GetAxis("Vertical");
            Vector3 pos = transform.position + Vector3.right * dx + Vector3.forward * dy;
            transform.position = pos;

            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                // Debug.Log(hit.transform);
                Destroy(hit.transform.gameObject);
        //     if(NetworkManager.Singleton.IsServer)
        //     {
        //         serverPosition.Value = pos;
        //     }
        //     else
        //         ChangePositionServerRpc(pos);
        // }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.transform);
    }
    // public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

    // public override void OnNetworkSpawn()
    // {
    //     if (IsOwner)
    //     {
    //         Move();
    //     }
    // }

    // public void Move()
    // {
    //     if (NetworkManager.Singleton.IsServer)
    //     {
    //         var randomPosition = GetRandomPositionOnPlane();
    //         transform.position = randomPosition;
    //         Position.Value = randomPosition;
    //     }
    //     else
    //     {
    //         SubmitPositionRequestServerRpc();
    //     }
    // }

    // [ServerRpc]
    // void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    // {
    //     Position.Value = GetRandomPositionOnPlane();
    // }

    // static Vector3 GetRandomPositionOnPlane()
    // {
    //     return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    // }

    // void Update()
    // {
    //     transform.position = Position.Value;
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OneSidePortal : MonoBehaviour
{
    Transform player;
    public Transform spawnPos;
    public UnityEvent onTeleport;

    bool teleport;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // Debug.Log(other.name);
            player = other.transform;
            teleport = true;
        }
    }

    private void FixedUpdate()
    {
        if(teleport)
        {
            player.position = spawnPos.position;
            player.forward = spawnPos.forward;
            teleport = false;
            onTeleport.Invoke();
        }
    }
}

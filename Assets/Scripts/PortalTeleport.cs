using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    bool playerOverlapping;
    Transform player;
    public Transform linkedPortal;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOverlapping = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOverlapping = false;
        }
    }

    private void FixedUpdate()
    {
        if(playerOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;

            if( Vector3.Dot( transform.up, portalToPlayer) < 0)
            {
                //Vector3 playerOffset = player.position - transform.position;
                //playerOffset = transform.parent.InverseTransformDirection(playerOffset);
                //playerOffset = linkedPortal.parent.TransformDirection(playerOffset);

                player.position = linkedPortal.position + portalToPlayer;
                player.forward = linkedPortal.up;

                playerOverlapping = false;
            }
        }
    }
}

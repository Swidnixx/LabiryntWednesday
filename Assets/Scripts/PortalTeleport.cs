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
                portalToPlayer = transform.parent.InverseTransformDirection(portalToPlayer);
                portalToPlayer = linkedPortal.parent.TransformDirection(portalToPlayer);

                player.position = linkedPortal.position + portalToPlayer;

                Vector3 playerForward = transform.parent.InverseTransformDirection(player.forward);
                playerForward = linkedPortal.parent.TransformDirection(playerForward);

                player.forward = playerForward;

                playerOverlapping = false;
            }
        }
    }
}

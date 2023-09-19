using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActivator : MonoBehaviour
{
    public LayerMask enviroMask;
    Transform player;
    Portal portal;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        portal = GetComponent<Portal>();

        StartCoroutine(DetectPlayer());
    }

    IEnumerator DetectPlayer()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 pos = transform.position + transform.forward * 0.5f;
            Vector3 portalToPlayer = player.position - pos;
            float distance = portalToPlayer.magnitude;

            Ray ray = new Ray(pos, portalToPlayer.normalized);
            bool hitSmth = Physics.Raycast(ray, distance, enviroMask, QueryTriggerInteraction.Ignore);

            if(hitSmth)
            {
                portal.Deactivate();
            }
            else
            {
                portal.Activate();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pacman : MonoBehaviour
{
    public float speed = 1;
    public float tileSize = 5;
    public LayerMask wallMask;
    public Transform player;

    [Header("Player Detection")]
    public float spherecastRadius = 1;
    public float detectionDistance = 10;
    public LayerMask playerMask;
    public float chasingCooldown = 5;

    public AudioSource audioSource;
    public AudioClip[] clips;

    Vector3 dir;
    Vector3 currentPos, targetPos;

    float t;

    public void Start()
    {
        currentPos = transform.position;
        ChooseRandomDirection();
        transform.forward = dir;
        t = 0;
    }

    bool playerCaught = false;
    public void Update()
    {
        if (!playerCaught)
        {
            DetectPlayer();
            Move();
            UpdateState(); 
        }
        else
        {
            transform.forward = player.position - transform.position;
            transform.Rotate( new Vector3( 
                Mathf.Sin(Time.time * 25) * 25 + Random.Range(0, 10), 
                Mathf.Cos(Time.time * 25) * 25 + Random.Range(0, 10), 
                Mathf.Sin(Time.time * 25) * 25 + Random.Range(0, 10)
                )
                );

            Vector3 upPos = currentPos + new Vector3(0.15f, 1, 0.15f);
            Vector3 downPos = currentPos + Vector3.Scale((player.position - currentPos), Vector3.one * 0.5f);

            transform.position = Vector3.Lerp(upPos, downPos, (Mathf.Sin(Time.time * 5) +1 ) * 0.5f);
        }
    }
    IEnumerator PlayHarshSounds()
    {
        while(true)
        {
            audioSource.PlayOneShot(clips[0]);
            yield return new WaitForSeconds(clips[0].length);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerCaught = true;
            StartCoroutine(PlayHarshSounds());
        }
    }

    float chaseCountdown;
    bool playerDetected, wandering;
    Vector3 detectionSpot;
    private void DetectPlayer()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, dir);
        playerDetected = Physics.SphereCast( ray, spherecastRadius, out hit, detectionDistance, playerMask);

        if( !playerDetected )
        {
            if( !wandering )
            {
                chaseCountdown -= Time.deltaTime;
                if(chaseCountdown <= 0)
                {
                    wandering = true;
                }
            }
        }
        else
        {
            wandering = false;
            chaseCountdown = chasingCooldown;
            detectionSpot = hit.point;
        }
    }

    private void UpdateState()
    {
        if (t >= 1)
        {
            currentPos = targetPos;
            ChooseDirection();
            transform.forward = dir;
            t = 0;
        }
    }

    private void ChooseDirection()
    {
        if (wandering)
            ChooseRandomDirection();
        else
        {
            FindAfterPlayerDirection();
        }
    }

    private void FindAfterPlayerDirection()
    {
        Vector3 toPlayerDir = player.position - transform.position;

        Vector3[] directions = { Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right };
        bool[] availableDirs = new bool[directions.Length];

        for (int i = 0; i < directions.Length; i++)
        {
            availableDirs[i] = !Physics.Raycast(currentPos, directions[i], tileSize * 0.5f + 0.1f, wallMask);
        }

        int[] indices = (new int[] { 0, 1, 2, 3 }).OrderByDescending(i => Vector3.Dot(directions[i], toPlayerDir)).ToArray();

        int index = indices.First( i => availableDirs[i]);

        dir = directions[index];
        targetPos = currentPos + dir.normalized * tileSize;
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(currentPos, targetPos, t);
        // t += Time.deltaTime;
        t += Time.deltaTime * speed;
    }

    private void ChooseRandomDirection()
    {
        Vector3[] directions = { Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right };
        bool[] availableDirs = new bool[directions.Length];

        for (int i = 0; i < directions.Length; i++)
        {
            availableDirs[i] = !Physics.Raycast(currentPos, directions[i], tileSize * 0.5f + 0.1f, wallMask);
        }

        int[] indices = (new int[] { 0, 1, 2, 3 }).Where(i => availableDirs[i]).ToArray();

        int index = indices[UnityEngine.Random.Range(0, indices.Length)];
        dir = directions[index];
        targetPos = currentPos + dir.normalized * tileSize;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(currentPos, 1);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(targetPos, 1);

        if(playerDetected)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionSpot, 1);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + dir * detectionDistance, 1);
        }
    }

}


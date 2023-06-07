using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public float tileSize = 5;
    public LayerMask wallMask;
    public float speed = 1;

    Vector3 dir;
    Vector3 currentPos, targetPos;

    float t;

    private void Start()
    {
        currentPos = transform.position;
        ChooseRandomDirection();
        transform.forward = dir;
        t = 0;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(currentPos, targetPos, t);
       // t += Time.deltaTime;
        t += Time.deltaTime * speed;

        if( t >= 1)
        {
            currentPos = targetPos;
            ChooseRandomDirection();
            transform.forward = dir;
            t = 0;
        }
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
}

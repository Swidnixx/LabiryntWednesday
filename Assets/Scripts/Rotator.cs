using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 1;
    public Vector3 axis = Vector3.up;

    public void Update()
    {
        transform.Rotate( axis, speed * Time.deltaTime );
    }
}

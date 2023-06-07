using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public PlayerMovement pm;
    public float fallTime = 1;
    public float fallDegree = 90;

    bool killed = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Deadly") && !killed)
        {
            killed = true;
            Death();
        }
    }

    private void Death()
    {
        pm.enabled = false;

        StartCoroutine(FallDown());
    }

    IEnumerator FallDown()
    {
        for (float i = 0; i < fallTime; i+=Time.deltaTime)
        {
            transform.Rotate(Vector3.right * fallDegree * Time.deltaTime, Space.Self);
            yield return null;
        }
        StartCoroutine(LayDown());
    }
    IEnumerator LayDown()
    {
        for (float i = 0; i < fallTime; i+=Time.deltaTime)
        {
            transform.Rotate(Vector3.up * fallDegree* Time.deltaTime, Space.Self);
            yield return null;
        }

        GameManager.Instance.GameOver();
    }
}

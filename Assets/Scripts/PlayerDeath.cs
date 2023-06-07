using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public PlayerMovement pm;
    public float fallSpeed = 1;

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
        for (int i = 0; i < 500; i++)
        {
            transform.Rotate(Vector3.right * fallSpeed * Time.deltaTime, Space.Self);
            yield return null;
        }
        StartCoroutine(LayDown());
    }
    IEnumerator LayDown()
    {
        for (int i = 0; i < 500; i++)
        {
            transform.Rotate(Vector3.up * fallSpeed * Time.deltaTime, Space.Self);
            yield return null;
        }

        GameManager.Instance.GameOver();
    }
}

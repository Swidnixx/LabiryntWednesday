using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerSensor : MonoBehaviour
{
    public string otherTag = "Player";

    public UnityEvent OnDetected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(otherTag))
        {
            OnDetected.Invoke();
        }
    }
}

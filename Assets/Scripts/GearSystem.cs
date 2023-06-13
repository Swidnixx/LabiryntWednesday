using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GearSystem : MonoBehaviour
{
    Rotator[] rotators;
    float[] speeds;

    private void Start()
    {
        rotators = GetComponentsInChildren<Rotator>();
        speeds = rotators.Select(r => r.speed).ToArray();
        Off();
    }

    public void On()
    {
        for (int i = 0; i < rotators.Length; i++)
        {
            rotators[i].speed = speeds[i];
        }
    }
    public void Off()
    {
        Array.ForEach(rotators, r => r.speed = 0);
    }
}

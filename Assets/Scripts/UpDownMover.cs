using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMover : MonoBehaviour
{
    public Transform movingPart;
    public Transform topPos, downPos;
    public float speed = 1;
    public float topWait = 0;
    public float downWait = 1;

    bool goinUp;
    float t;

    private void Update()
    {
        movingPart.position = Vector3.MoveTowards(movingPart.position, goinUp ? topPos.position : downPos.position, Time.deltaTime * speed);

        if (goinUp && Vector2.Distance(movingPart.position, topPos.position) < 0.01f)
        {
            t += Time.deltaTime;
            if (t >= topWait)
            {
                goinUp = false;
                t = 0;
            }
        }

        if (!goinUp && Vector2.Distance(movingPart.position, downPos.position) < 0.01f)
        {
            t += Time.deltaTime;
            if (t >= downWait)
            {
                goinUp = true;
                t = 0;
            }
        }
    }
}

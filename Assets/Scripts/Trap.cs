using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform spikes;
    public float speed = 1;
    public float offTime = 1, onTime = 1;
    [Range(0,1)]
    public float offset;

    bool on;
    float t;

    private void Start()
    {
        t = offset;
        StartCoroutine(Cycle());
    }

    public void AdjustOffTime(float offset)
    {
        offTime += offset;
    }

    private IEnumerator Cycle()
    {
        while (true)
        {
            t = Mathf.Clamp01(t + (on ? Time.deltaTime : -Time.deltaTime) * speed);

            if (t == 1 || t == 0)
            {
                yield return new WaitForSeconds(!on ? offTime : onTime);
                on = !on;
            }

            float y = Mathf.Lerp(0.1f, 10, t * t);
            spikes.localScale = new Vector3(1, y, 1);
            yield return null;
        }
    }
}

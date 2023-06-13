using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Pickup
{
    public int time = 10;

    protected override void OnPicked()
    {
        base.OnPicked();
        GameManager.Instance.AddTime(time);
    }
}

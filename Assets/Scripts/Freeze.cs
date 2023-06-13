using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Pickup
{
    public int time = 10;

    protected override void OnPicked()
    {
        base.OnPicked();
        GameManager.Instance.FreezeTime(time);
    }
}

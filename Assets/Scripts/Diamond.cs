using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Pickup
{
    protected override void OnPicked()
    {
        base.OnPicked();
        GameManager.Instance.AddDiamond();
    }
}

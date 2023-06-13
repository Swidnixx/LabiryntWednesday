using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickup
{
    public KeyType type;

    protected override void OnPicked()
    {
        base.OnPicked();
        GameManager.Instance.AddKey( type );
    }

    public enum KeyType { Red, Green, Gold }
}

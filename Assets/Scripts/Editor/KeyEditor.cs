using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Key))]
public class KeyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //if(GUILayout.Button("SetupId"))
        //{
        //    ((Key)target).Register();
        //}

        if (GUILayout.Button("PrintList"))
        {
            Key.PrintList();
        }
    }
}

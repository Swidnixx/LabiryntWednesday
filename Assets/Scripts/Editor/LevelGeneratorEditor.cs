using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelGenerator lgen = (LevelGenerator)target;

        if(GUILayout.Button("Generate"))
        {
            lgen.Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            lgen.Clear();
        }
    }
}

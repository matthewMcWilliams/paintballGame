using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Randomizable))]
public class PathDataSOInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Randomize"))
        {
            var path = target as Randomizable;
            path.Randomize();
        }
    }
}

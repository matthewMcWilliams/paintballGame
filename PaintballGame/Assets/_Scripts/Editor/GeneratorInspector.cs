using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Generator))]
public class GeneratorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var generator = target as Generator;
        if (GUILayout.Button("Generate"))
        {
            generator.Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            generator.Clear();
        }
    }
}

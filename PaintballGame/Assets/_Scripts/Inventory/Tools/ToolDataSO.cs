using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolDataSO : ScriptableObject
{
    public string ToolName = "Untitle Tool";
    public abstract void UseTool(Transform playerRoot);
}

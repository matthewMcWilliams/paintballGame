using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : Selector
{
    public BehaviourTree() : base ()
    {
        name = "Tree";
    }

    public BehaviourTree(string n) : base(n)
    {
        name = n;
    }

    private void Update()
    {
        Process();
    }

    public void PrintTree()
    {
        Debug.Log(PrintNode(0));
    }
}

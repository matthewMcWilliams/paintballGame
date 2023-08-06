using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> children = new List<Node>();
    [HideInInspector] public int currentChild = 0;
    public string name;
    
    public enum Status
    {
        SUCCESS, RUNNING, FAIL
    };

    public Node() { }

    public Node(string n)
    {
        name = n;
    }

    public void AddChild(Node n)
    {
        children.Add(n);
    }

    public virtual Status Process()
    {
        return Status.RUNNING;
    }

    public string PrintNode(int indention)
    {
        string printOut = name;

        if (children.Count > 0)
        {
            printOut += " ( \n     ";

            foreach (var child in children)
            {
                printOut += child.PrintNode(indention + 1) + ", ";
            }

            printOut += "\n) \n";
        }

        return printOut;
        
    }

    public virtual void SwitchToNode()
    {
        
    }
}

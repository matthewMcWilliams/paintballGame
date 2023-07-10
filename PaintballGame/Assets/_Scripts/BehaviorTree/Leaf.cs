using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{

    public List<Action> actions;
    public List<Decision> decisions;

    public Leaf() { }


    public Leaf(string n)
    {
        name = n;
    }

    public override Status Process()
    {
        Debug.Log("Process " + name);
        foreach (var action in actions)
        {
            action.TakeAction();
        }
        bool running = false;
        foreach (var decision in decisions)
        {
            Status d = decision.MakeDecision();
            if (d == Status.SUCCESS)
            {
                return Status.SUCCESS;
            }
            if (d == Status.FAIL)
            {
                return Status.FAIL;
            }
            running |= d == Status.RUNNING;
        }
        if (running)
        {
            return Status.RUNNING;
        }
        return Status.SUCCESS;
        
    }

    public override void SwitchToNode()
    {
        foreach (var action in actions)
        {
            action.SwitchToAction();
        }
    }

}

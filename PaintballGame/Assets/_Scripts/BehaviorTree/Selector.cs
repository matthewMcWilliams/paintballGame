using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector()
    {
        name = "Selector";
    }

    public Selector(string n)
    {
        name = n;
    }

    public override Status Process()
    {
        Status childStatus = children[currentChild].Process();
        switch (childStatus)
        {
            case Status.SUCCESS:
                {
                    currentChild = 0;
                    return Status.SUCCESS;
                }
            case Status.RUNNING:
                {
                    return Status.RUNNING;
                }
            case Status.FAIL:
                {
                    currentChild++;
                    if (currentChild >= children.Count)
                    {
                        currentChild = 0;
                        return Status.FAIL; 
                    }
                    children[currentChild].SwitchToNode();
                }
                break;
            default:
                break;
        }

        return Status.RUNNING;
    }

    public override void SwitchToNode()
    {
        children[currentChild].SwitchToNode();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public override Status Process()
    {
        Status childStatus = children[currentChild].Process();
        switch (childStatus)
        {
            case Status.SUCCESS:
                {
                    currentChild++;
                    if (currentChild >= children.Count)
                    {
                        currentChild = 0;
                        return Status.SUCCESS;
                    }
                    SwitchToNode();
                }
                break;
            case Status.RUNNING:
                {
                    return Status.RUNNING;
                }
            case Status.FAIL:
                {
                    return Status.FAIL;
                }
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

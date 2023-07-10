using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionConstantRunning : Decision
{
    public override Node.Status MakeDecision()
    {
        return Node.Status.RUNNING;
    }

}

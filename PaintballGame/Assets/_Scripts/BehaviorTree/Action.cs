using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public virtual void SwitchToAction() { }
    public abstract void TakeAction();
}

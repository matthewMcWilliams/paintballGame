using UnityEngine;

public abstract class Decision : MonoBehaviour
{
    public abstract Node.Status MakeDecision();
}
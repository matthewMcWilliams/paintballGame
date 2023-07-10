using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnStartStop : NetworkBehaviour
{
    public override void OnStartClient()
    {
        //Debug.Log("CLIENT START");
        Destroy(gameObject);
    }

    public override void OnStopClient()
    {
        //Debug.Log("CLIENT END");
        Destroy(gameObject);
    }
}

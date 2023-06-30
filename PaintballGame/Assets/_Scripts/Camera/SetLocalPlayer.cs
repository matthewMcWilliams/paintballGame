using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLocalPlayer : NetworkBehaviour
{
    public override void OnStartClient()
    {
        Debug.Log("CONNECTED");
        Transform localPlayer = NetworkClient.localPlayer.transform;
        GetComponent<CinemachineTargetGroup>().m_Targets[0].target = localPlayer;
    }
}

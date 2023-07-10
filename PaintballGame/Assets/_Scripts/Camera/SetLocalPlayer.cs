using Cinemachine;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLocalPlayer : NetworkBehaviour
{
    public override void OnStartClient()
    {
        //Debug.Log("CONNECTED");
        if (NetworkClient.localPlayer == null)
        {
            StartCoroutine(CouldNotConnectCoroutine());
            return;
        }
        Transform localPlayer = NetworkClient.localPlayer.transform;
        GetComponent<CinemachineTargetGroup>().m_Targets[0].target = localPlayer;
        //Debug.Log(localPlayer.position);
    }

    private IEnumerator CouldNotConnectCoroutine()
    {
        yield return new WaitForEndOfFrame();
        if (NetworkClient.localPlayer != null)
        {
            OnStartClient();
        }
    }
}

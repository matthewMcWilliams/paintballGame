using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToLocalPlayerMousePos : NetworkBehaviour
{
    private AgentInput _agentInput;
    public override void OnStartClient()
    {
        if (NetworkClient.localPlayer == null)
            return;
        _agentInput = NetworkClient.localPlayer.GetComponent<AgentInput>();
    }

    private void Update()
    {
        if (_agentInput != null)
            transform.position = _agentInput.GetMousePositionWorld();
    }
}

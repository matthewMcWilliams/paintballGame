using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : NetworkBehaviour
{
    private Transform _player;

    public override void OnStartClient()
    {
        _player = NetworkClient.localPlayer.transform;
    }

    private void LateUpdate()
    {
        if (_player == null)
            return;
        Vector3 newPosition = _player.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}

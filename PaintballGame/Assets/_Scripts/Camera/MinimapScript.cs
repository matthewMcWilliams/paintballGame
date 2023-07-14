using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : NetworkBehaviour
{
    private Transform _player;

    public override void OnStartClient()
    {
        var localPlayer = NetworkClient.localPlayer;
        if (localPlayer != null)
        {
            _player = localPlayer.transform;

        }
    }

    private void LateUpdate()
    {
        if (_player == null)
        {
            OnStartClient();
            return;
        }
        SetPosition();
    }


    private void SetPosition()
    {
        Vector3 newPosition = _player.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}

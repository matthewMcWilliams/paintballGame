using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHitbox : NetworkBehaviour
{
    public event System.Action OnDie;

    [SerializeField] private LayerMask _bulletMask;
    [SerializeField] private GameObject _player;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isServer && _bulletMask == (_bulletMask | (1 << collider.gameObject.layer)))
        {
            DestroyPlayer();
            Destroy(collider.gameObject);
            OnDie?.Invoke();
        }
    }

    [ClientRpc]
    private void DestroyPlayer()
    {
        Destroy(_player);
    }
}

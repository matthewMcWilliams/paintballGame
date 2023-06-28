using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHitbox : NetworkBehaviour
{
    [SerializeField] private LayerMask _bulletMask;
    [SerializeField] private GameObject _player;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isLocalPlayer && _bulletMask == (_bulletMask | (1 << collider.gameObject.layer)))
        {
            Destroy(_player);
            Destroy(collider.gameObject);
        }
    }

    [Command]
    void Respawn(GameObject go)
    {
        Transform newPos = NetworkManager.singleton.GetStartPosition();
        go.transform.position = newPos.position;
    }
}

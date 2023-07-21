using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class AgentHitbox : NetworkBehaviour
{
    public event System.Action<Transform> OnDie;

    [SerializeField] private UnityEvent _onPlayerDie, _onBounce;
    [SerializeField] private LayerMask _bulletMask;
    [SerializeField] private GameObject _player;

    private Vector2 startPos;

    private AgentInventoryManager _inventory;

    private void Awake()
    {
        _inventory = transform.root.GetComponent<AgentInventoryManager>();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CheckForDeath(collider);
    }

    private void CheckForDeath(Collider2D collider)
    {
        if (isServer && _bulletMask == (_bulletMask | (1 << collider.gameObject.layer)))
        {
            if (Random.Range(0f, 1f) > GetBounceChance(collider))
            {
                Die(collider);
            }
            else
            {
                Bounce(collider);
            }
        }
    }

    private float GetBounceChance(Collider2D collider)
    {
        if (collider.TryGetComponent(out Bullet bullet))
        {
            return (_inventory.ArmorData.BounceChance - bullet.BulletData.BounceResistance);
        }
        return _inventory.ArmorData.BounceChance;
    }

    private void Bounce(Collider2D collider)
    {
        Destroy(collider.gameObject);
        _onBounce?.Invoke();
    }

    private void Die(Collider2D collider)
    {
        DestroyPlayer();
        Destroy(collider.gameObject);
        OnDie?.Invoke(transform.root);
        _onPlayerDie?.Invoke();
    }

    [ClientRpc]
    private void DestroyPlayer()
    {
        Destroy(_player);
    }
}

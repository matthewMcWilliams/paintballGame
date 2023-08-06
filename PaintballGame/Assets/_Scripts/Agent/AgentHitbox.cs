using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
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
                Destroy(collider.gameObject);
                Die();
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

    public void Die()
    {
        OnDie?.Invoke(transform.root);
        _onPlayerDie?.Invoke();

        Bounds respawnRoom = GameManager.Instance.RespawnRoom.bounds ;
        Vector3 newPos = new Vector3(
                    Random.Range(respawnRoom.min.x, respawnRoom.max.x),
                    Random.Range(respawnRoom.min.y, respawnRoom.max.y)
                    );

        if (transform.root.GetComponent<AIBehavior>())
        {
            var p = Instantiate(GameManager.Instance.IdleBotPrefab, newPos, Quaternion.identity);
            p.transform.root.GetComponentInChildren<AgentRenderer>().TeamColor = transform.root.GetComponentInChildren<AgentRenderer>().TeamColor;

            DestroyPlayer();
            return;
        }

        if (isLocalPlayer)
        {
            GameManager.Instance.GameVCam.Priority = 10;
        }

        //respawnRoom.center += GameManager.Instance.RespawnRoom.transform.position;
        //Debug.Log(respawnRoom);

        transform.root.position = newPos;

    }

    [ClientRpc]
    private void DestroyPlayer()
    {
        Destroy(_player);
    }
}

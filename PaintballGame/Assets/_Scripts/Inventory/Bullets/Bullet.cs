using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : NetworkBehaviour
{
    [HideInInspector, SyncVar] public float DistanceToTravel;

    [SerializeField] private UnityEvent _bulletDestroyed, _bulletHitTreeBush;

    public BulletDataSO BulletData;

    [SerializeField] private float _goThroughBushChance = 0.5f;
    [SerializeField] private LayerMask _bushLayerMask;
    [SerializeField] private float _goThroughTreeChance = 0f;
    [SerializeField] private LayerMask _treeMask;

    Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isServer && Vector3.Distance(_startPosition, transform.position) > DistanceToTravel)
        {
            DestroyThisBullet();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isServer)   
            DestroyThisBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isServer)
            CheckCollision(collision);
    }

    private void CheckCollision(Collider2D collision)
    {
        if (_bushLayerMask.Contains(collision.gameObject.layer) && Random.Range(0f, 1f) > _goThroughBushChance)
        {
            DestroyThisBullet();
            _bulletHitTreeBush?.Invoke();
        }
        if (_treeMask.Contains(collision.gameObject.layer) && Random.Range(0f, 1f) > _goThroughTreeChance)
        {
            DestroyThisBullet();
            _bulletHitTreeBush?.Invoke();
        }
    }

    private void DestroyThisBullet()
    {
        SetPositionAndDestroy(transform.position);
    }

    [ClientRpc]
    private void SetPositionAndDestroy(Vector3 position)
    {
        Destroy(gameObject, 0.01f);
        transform.position = position;
    }

    private void OnDestroy()
    {
        _bulletDestroyed?.Invoke();
        
    }
}

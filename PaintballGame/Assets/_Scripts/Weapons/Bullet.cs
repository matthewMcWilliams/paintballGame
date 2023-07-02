using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private FeedbackManager _splatFeedback;

    [SerializeField] private float _goThroughBushChance = 0.5f;
    [SerializeField] private LayerMask _bushLayerMask;
    [SerializeField] private float _goThroughTreeChance = 0f;
    [SerializeField] private LayerMask _treeMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }

    private void CheckCollision(Collider2D collision)
    {
        if (_bushLayerMask.Contains(collision.gameObject.layer) && Random.Range(0f, 1f) > _goThroughBushChance)
        {
            DestroyBullet();
        }
        if (_treeMask.Contains(collision.gameObject.layer) && Random.Range(0f, 1f) > _goThroughTreeChance)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject, 0.01f);
    }

    private void OnDestroy()
    {
        _splatFeedback.GiveFeedback();
        
    }
}

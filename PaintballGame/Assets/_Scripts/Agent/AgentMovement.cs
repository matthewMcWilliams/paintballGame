using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AgentMovement : NetworkBehaviour
{
    [SerializeField, Range(0,15)] private float _maxSpeed, _slowSpeedBush, _slowSpeedTree;
    [SerializeField, Range(0,40)] private float _maxAcceleration;
    [SerializeField] private LayerMask _bushLayerMask, _treeLayerMask;

    private float _speed;
    private bool _colliding = false;

    private Rigidbody2D _rb;
    private AgentInput _agentInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _agentInput = GetComponent<AgentInput>();
        _speed = _maxSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_bushLayerMask.Contains(collision.gameObject.layer))
        {
            _speed = _slowSpeedBush;
            _colliding = true;
        }
        if (_treeLayerMask.Contains(collision.gameObject.layer))
        {
            _speed = _slowSpeedTree;
            _colliding = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_bushLayerMask.Contains(collision.gameObject.layer))
        {
            _speed = _slowSpeedBush;
            _colliding = true;
        }
        if (_treeLayerMask.Contains(collision.gameObject.layer))
        {
            _speed = _slowSpeedTree;
            _colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_colliding)
            _speed = _maxSpeed;
    }

    

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            var targetInput = _agentInput.GetMovementInput() * _speed;
            _rb.velocity = Vector2.MoveTowards(_rb.velocity, targetInput, _maxAcceleration * Time.deltaTime);
            _colliding = false;
        }
    }
}

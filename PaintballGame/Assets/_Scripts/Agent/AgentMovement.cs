using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AgentMovement : NetworkBehaviour
{
    [SerializeField] private MovementDataSO _movementData;

    private float _speed;
    private bool _colliding = false;

    private Rigidbody2D _rb;
    private IInputtable _agentInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _agentInput = GetComponent<IInputtable>();
        _speed = _movementData.MaxSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_movementData.BushLayerMask.Contains(collision.gameObject.layer))
        {
            _speed = _movementData.SlowSpeedBush;
            _colliding = true;
        }
        if (_movementData.TreeLayerMask.Contains(collision.gameObject.layer))
        {
            _speed = _movementData.SlowSpeedTree;
            _colliding = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_movementData.BushLayerMask.Contains(collision.gameObject.layer))
        {
            _speed = _movementData.SlowSpeedBush;
            _colliding = true;
        }
        if (_movementData.TreeLayerMask.Contains(collision.gameObject.layer))
        {
            _speed = _movementData.SlowSpeedTree;
            _colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_colliding)
            _speed = _movementData.MaxSpeed;
    }

    

    private void FixedUpdate()
    {
        var targetInput = _agentInput.GetMovementInput() * _speed;
        if ((_rb.velocity.sqrMagnitude < targetInput.sqrMagnitude && !_colliding) || targetInput != Vector2.zero)
        {
            _rb.velocity = Vector2.MoveTowards(_rb.velocity, targetInput, _movementData.MaxAcceleration * Time.deltaTime); 
        } else
        {
            _rb.velocity = _rb.velocity.normalized * targetInput.magnitude;
            _rb.velocity = Vector2.MoveTowards(_rb.velocity, targetInput, _movementData.MaxAcceleration * Time.deltaTime); 
        }
        _colliding = false;
    }
}

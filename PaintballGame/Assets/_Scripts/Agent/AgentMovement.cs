using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AgentMovement : NetworkBehaviour
{
    [SerializeField, Range(0,15)] private float _maxSpeed;
    [SerializeField, Range(0,40)] private float _maxAcceleration;

    private Rigidbody2D _rb;
    private AgentInput _agentInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _agentInput = GetComponent<AgentInput>();
    }


    private void FixedUpdate()
    {
        if (this.isLocalPlayer)
        {
            var targetInput = _agentInput.GetMovementInput() * _maxSpeed;
            _rb.velocity = Vector2.MoveTowards(_rb.velocity, targetInput, _maxAcceleration * Time.deltaTime); 
        }
    }
}

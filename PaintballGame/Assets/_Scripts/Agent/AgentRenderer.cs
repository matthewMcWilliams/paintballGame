using Mirror;
using System;
using UnityEngine;

public class AgentRenderer : NetworkBehaviour
{
    [field: SyncVar] public bool Visible { set; get; } = true;

    private Color _teamColor = Color.white;
    public Color TeamColor
    {
        get => _teamColor; 
        set
        {
            _teamColor = value;
        }
    }

    [SerializeField] private string _runParameter = "Running";
    [SerializeField] private string _speedParameter = "RunSpeed";

    [SyncVar] private bool _flipped = false;

    private Animator _controller;
    private SpriteRenderer _spriteRendererBody, _spriteRendererHead;
    private Rigidbody2D _rb;
    private IInputtable _agentInput;

    private void Awake()
    {
        _controller = GetComponent<Animator>();
        _spriteRendererBody = GetComponent<SpriteRenderer>();
        _spriteRendererHead = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _rb = GetComponentInParent<Rigidbody2D>();
        _agentInput = transform.root.GetComponent<IInputtable>();
    }

    private void Update()
    {
        UpdateAnimator();
        UpdateDirection();
        UpdateVisibility();
        UpdateColor();
    }

    private void UpdateColor()
    {
        _spriteRendererBody.color = _teamColor;
    }

    private void UpdateVisibility()
    {
        SpriteRenderer[] renderers = transform.root.GetComponentsInChildren<SpriteRenderer>();
        foreach (var renderer in renderers)
        {
            renderer.enabled = Visible;
        }
    }

    private void UpdateDirection()
    {
        _flipped = (_agentInput.GetMousePositionWorld() - (Vector2)transform.position).x < 0;
        FlipCharacter(_flipped);
    }

    private void FlipCharacter(bool flipped)
    {
        _spriteRendererBody.flipX = flipped;
        _spriteRendererHead.flipX = flipped;
    }


    private void UpdateAnimator()
    {
        bool running = _agentInput.GetMovementInput().magnitude > 0.1f;
        _controller.SetBool(_runParameter, running);
        float dir = Mathf.Sign(_rb.velocity.x) * (_spriteRendererBody.flipX ? 1 : -1);
        _controller.SetFloat(_speedParameter, dir);
    }
}

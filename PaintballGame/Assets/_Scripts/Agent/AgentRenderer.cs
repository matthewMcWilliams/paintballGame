using Mirror;
using UnityEngine;

public class AgentRenderer : NetworkBehaviour
{
    [SerializeField] private string _runParameter = "Running";
    [SerializeField] private string _speedParameter = "RunSpeed";

    [SyncVar] private bool _flipped = false;

    private Animator _controller;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private IInputtable _agentInput;

    private void Awake()
    {
        _controller = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponentInParent<Rigidbody2D>();
        _agentInput = transform.root.GetComponent<IInputtable>();
    }

    private void Update()
    {
        UpdateAnimator();
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        _flipped = (_agentInput.GetMousePositionWorld() - (Vector2)transform.position).x < 0;
        FlipCharacter(_flipped);
    }

    private void FlipCharacter(bool flipped)
    {
        _spriteRenderer.flipX = flipped;
    }

    private void UpdateAnimator()
    {
        bool running = _agentInput.GetMovementInput().magnitude > 0.1f;
        _controller.SetBool(_runParameter, running);
        float dir = Mathf.Sign(_rb.velocity.x) * (_spriteRenderer.flipX?1:-1);
        _controller.SetFloat(_speedParameter, dir);
    }
}

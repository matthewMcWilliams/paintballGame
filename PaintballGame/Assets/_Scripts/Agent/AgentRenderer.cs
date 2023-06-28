using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    [SerializeField] private string _runParameter = "Running";
    [SerializeField] private string _speedParameter = "RunSpeed";
    [SerializeField] private AgentInput _agentInput;

    private Animator _controller;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _controller = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateAnimator();
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        bool flip = (_agentInput.GetMousePositionWorld() - (Vector2)transform.position).x < 0;
        _spriteRenderer.flipX = flip;
    }

    private void UpdateAnimator()
    {
        bool running = _agentInput.GetMovementInput().magnitude > 0.1f;
        _controller.SetBool(_runParameter, running);
        Vector2 velocity = _rb.velocity;
        float dir = Mathf.Sign(velocity.x) * (_spriteRenderer.flipX?1:0);
        _controller.SetFloat(_speedParameter, dir);
    }
}

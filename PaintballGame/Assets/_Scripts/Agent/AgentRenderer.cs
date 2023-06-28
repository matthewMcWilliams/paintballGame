using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    [SerializeField] private string _runParameter = "Running";
    [SerializeField] private AgentInput _agentInput;

    private Animator _controller;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _controller = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAimAtClosestPlayer : Action
{
    [SerializeField] private float _accuracy;
    [SerializeField] private float _aimSpeed;
    [SerializeField] private AnimationCurve _accuracyCurve;

    private Vector2 _target;

    private InputData _inputData;


    private void Awake()
    {
        _inputData = transform.root.GetComponent<InputData>();
    }

    public override void TakeAction()
    {
        _target = GetTarget();

        _inputData.MousePosWorld = Vector2.MoveTowards(_inputData.MousePosWorld, _target, _aimSpeed * Time.deltaTime);
    }

    public override void SwitchToAction()
    {
        _target = GetTarget();
    }

    private Vector2 GetTarget()
    {
        var player = PlayerManager.Instance.FindClosestOpponent(transform) ?? transform;
        Vector2 target = player.position;
        float angle = Mathf.Atan2(target.y, target.x);
        float distance = target.magnitude;
        angle += _accuracyCurve.Evaluate(Random.Range(0f, 1f)) * _accuracy;
        target = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        return target;
    }
}

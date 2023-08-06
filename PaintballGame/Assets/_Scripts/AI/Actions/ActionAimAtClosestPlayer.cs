using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        _inputData.MousePosWorld = (PlayerManager.Instance.FindClosestOpponent(transform) ?? transform).position;

        return;

        if (Vector3.Distance(_target, _inputData.MousePosWorld) < 0.5f)
        {
            _target = GetTarget(); 
        }

        _inputData.MousePosWorld = Vector3.RotateTowards((Vector3)_inputData.MousePosWorld - transform.position, (Vector3)_target - transform.position, _aimSpeed * Time.deltaTime, 10f) * _target.magnitude + transform.position;
    }

    public override void SwitchToAction()
    {
        _target = GetTarget();
    }

    private Vector2 GetTarget()
    {
        var player = PlayerManager.Instance.FindClosestOpponent(transform) ?? transform;
        Vector2 target = player.position;
        float angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
        float distance = Vector2.Distance(target, transform.position);
        angle += _accuracyCurve.Evaluate(Random.Range(0f, 1f)) * _accuracy;
        target = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * distance + transform.position;
        return target;
    }
}

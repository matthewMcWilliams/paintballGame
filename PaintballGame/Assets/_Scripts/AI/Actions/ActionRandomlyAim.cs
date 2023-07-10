using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionRandomlyAim : Action
{
    [SerializeField] private float _distance = 10f, _rotationSpeed = 1f, _turnDistance = 4f;

    private float _angle = 0f;
    private float _targetAngle = 0f;

    static float twoPi = Mathf.PI * 2;

    private InputData _inputData;


    private void Awake()
    {
        _inputData = transform.root.GetComponent<InputData>();
    }

    public override void TakeAction()
    {
        float distance = Mathf.Abs(_angle - _targetAngle);
        //Debug.Log(_angle);

        if (distance < _turnDistance || (distance > twoPi && distance < twoPi + _turnDistance))
        {
            _targetAngle = Random.Range(0f, twoPi);
        }
        _angle = Mathf.MoveTowardsAngle(_angle * Mathf.Rad2Deg, _targetAngle * Mathf.Rad2Deg, Time.deltaTime * _rotationSpeed) * Mathf.Deg2Rad;
        _inputData.MousePosWorld = transform.position + (new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle)) * _distance);
    }
}

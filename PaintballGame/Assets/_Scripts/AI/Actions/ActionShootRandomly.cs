using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionShootRandomly : Action
{
    [SerializeField] float _minDelay = 0.3f, _maxDelay = 1f;

    private bool _shooting = true;
    private float _timer = 0f, _delay = 1f;

    InputData _inputData;

    private void Awake()
    {
        _inputData = transform.root.GetComponent<InputData>();
    }

    public override void TakeAction()
    {
        _timer += Time.deltaTime;
        if (_timer > _delay)
        {
            _timer = 0f;
            _shooting = !_shooting;
            _delay = Random.Range(_minDelay, _maxDelay);
        }
        _inputData.FireHold = _shooting;
        _inputData.FirePress = _shooting;
    }
}

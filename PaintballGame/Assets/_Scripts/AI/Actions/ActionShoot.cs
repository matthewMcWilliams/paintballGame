using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionShoot : Action
{
    [SerializeField] float _firePressMin = 0.2f, _firePressMax = 0.3f;
    [SerializeField] private bool _firePress = true,  _fireHold = true;

    private float _timer = 0f;

    InputData _inputData;

    private void Awake()
    {
        _inputData = transform.root.GetComponent<InputData>();
    }

    public override void TakeAction()
    {
        _inputData.FireHold = _fireHold;
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer = Random.Range(_firePressMin, _firePressMax);
            _inputData.FirePress = _firePress;
        } else
        {
            _inputData.FirePress = false;
        }
    }
}

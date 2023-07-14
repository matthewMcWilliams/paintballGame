using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionShoot : Action
{
    [SerializeField] bool _firePress = true, _fireHold = true;

    InputData _inputData;

    private void Awake()
    {
        _inputData = transform.root.GetComponent<InputData>();
    }

    public override void TakeAction()
    {
        _inputData.FireHold = _fireHold;
        _inputData.FirePress = _firePress;
    }
}

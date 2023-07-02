using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackInstanciate : Feedback
{

    [SerializeField] private GameObject _objectToInstanciate;
    [SerializeField] private bool _randomRotation = false;
    [SerializeField] private float _positionRandomOffsetMultiplier = 0f;

    public override void Invoke()
    {
        Quaternion rotation = _randomRotation ? new Quaternion { eulerAngles = new(0, 0, Random.Range(0f, 360f)) } : Quaternion.identity;
        Instantiate(_objectToInstanciate, transform.position + (Vector3)Random.insideUnitCircle * _positionRandomOffsetMultiplier, rotation);
    }
}

using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackInstanciate : Feedback
{

    [SerializeField] private GameObject _objectToInstanciate;
    [SerializeField] private bool _randomRotation = false;
    [SerializeField] private float _positionRandomOffsetMultiplier = 0f;
    [SerializeField] private float _minScale = 0.8f, _maxScale = 1.3f;
    [SerializeField] private bool _sync = true;

    public override void Invoke()
    {
        Do();
    }



    private void Do()
    {
        Quaternion rotation = _randomRotation ? new Quaternion { eulerAngles = new(0, 0, Random.Range(0f, 360f)) } : Quaternion.identity;
        var thing = Instantiate(_objectToInstanciate, transform.position + (Vector3)Random.insideUnitCircle * _positionRandomOffsetMultiplier, rotation);
        float scale = Random.Range(_minScale, _maxScale);
        thing.transform.localScale = new(scale, scale, 1);
        if (NetworkServer.active)
        {
            NetworkServer.Spawn(thing); 
        }
    }
}

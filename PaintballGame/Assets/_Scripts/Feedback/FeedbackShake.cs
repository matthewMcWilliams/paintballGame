using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackShake : Feedback
{
    [SerializeField] private float _duration, _magnitude = 1f;
    [SerializeField] private Transform _target;
    [SerializeField] private bool _sync = true;
    [SerializeField] private Vector2 _targetOffset = Vector2.zero;

    public override void Invoke()
    {
        if (_sync)
        {
            DoTheDo();
            return;
        }
        Do();
    }

    [ClientRpc]
    private void DoTheDo()
    {
        Do();
    }

    private void Do()
    {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = _target.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            _target.localPosition = startPosition + (Vector3)_targetOffset + (Vector3)Random.insideUnitCircle * _magnitude;
            yield return null;
        }

        _target.localPosition = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackCameraShake : Feedback
{
    [SerializeField] private float _duration = 1f, _magnitude = 1f;
    [SerializeField] private bool _localPlayerOnly = false;

    public override void Invoke()
    {
        if ((!_localPlayerOnly && !isLocalPlayer))
            return;
        ScreenShake.Instance.Shake(_duration, _magnitude);
    }
}

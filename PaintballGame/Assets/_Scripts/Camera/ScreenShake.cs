using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class ScreenShake : Singleton<ScreenShake>
{
    [SerializeField] private AnimationCurve _shakeCurve;
    private CinemachineVirtualCamera _camera;
    [Min(0)] private float _shakeTimer = 0, _shakeLength = 1, _magnitude = 0;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            _shakeTimer = Mathf.Max(_shakeTimer, 0);

            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _shakeCurve.Evaluate(1 - (_shakeTimer / _shakeLength) * _magnitude);
        }
    }

    public void Shake(float timer, float intensity)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        _shakeTimer = timer;
        _shakeLength = timer;
        _magnitude = intensity;
    }
}
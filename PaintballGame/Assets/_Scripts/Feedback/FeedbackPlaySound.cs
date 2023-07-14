using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlaySound : Feedback
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _source;
    [SerializeField] private bool _sync = true;

    private void Awake()
    {
        if (_source == null)
        {
            _source = GetComponent<AudioSource>();
        }
    }

    public override void Invoke()
    {
        if (!_sync)
        {
            ServerOnlyPlay();
            return;
        }
        RPCPlay();
    }

    private void ServerOnlyPlay()
    {
        _source.Stop();
        _source.clip = _clip;
        _source.Play();
    }

    [ClientRpc]
    private void RPCPlay()
    {
        _source.Stop();
        _source.clip = _clip;
        _source.Play();
    }
}

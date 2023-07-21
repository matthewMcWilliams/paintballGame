using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameCountdownTimer : NetworkBehaviour
{
    [SerializeField] private float _countdownLength;
    [SerializeField] private bool _loop = true;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private UnityEvent<List<Transform>> _onTimerTimesUp;

    [SyncVar] private float _currentTime = 0f;

    private Collider2D _collider;
    private TextMeshPro _text;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _text = GetComponent<TextMeshPro>();
    }
    private void Start()
    {
        _currentTime = _countdownLength;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        _text.text = Mathf.Floor(_currentTime).ToString();

        if (_currentTime <= 0f)
        {
            if (_loop)
            {
                _currentTime = _countdownLength;
            }
            else
            {
                _currentTime = 0f;
            }

            Collider2D[] players = new Collider2D[8];
            _collider.GetContacts(players);
            List<Transform> playerList = players.Where(p => p != null && _playerMask.Contains(p.gameObject.layer)).Select(p => p.transform.root).Distinct().ToList();
            _onTimerTimesUp?.Invoke(playerList);

        }
    }
}

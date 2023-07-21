using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public int PlayerCount { get; private set; }
    public int StartPlayerCount { get; private set; }
    public string PlayerText => Mode != null ? Mode.Text : _defaultName;

    public GameMode Mode => _modes[_modeIndex];

    [SerializeField] private string _defaultName;

    [SerializeField] private List<GameMode> _modes;
    [SerializeField] private GameObject _botPrefab;
    [SerializeField] private CinemachineVirtualCamera _gameVCam;

    [SyncVar] private int _modeIndex;

    public List<Transform> Players;

    public void StartRound(List<Transform> players)
    {
        if (players.Count <= 0)
        {
            return;
        }
        Players = players;
        _modeIndex = Random.Range(0, _modes.Count);

        PlayerCount = 0;
        StartPlayerCount = 0;

        if (isServer)
        {
            if (players.Count < Mode.MinPlayers)
            {
                for (int i = players.Count; i < Mode.MinPlayers; i++)
                {
                    var newBot = Instantiate(_botPrefab);
                    players.Add(newBot.transform);
                    NetworkServer.Spawn(newBot);
                }
            }
            foreach (var player in players)
            {
                PlayerCount++;
                StartPlayerCount++;
                player.GetComponentInChildren<AgentHitbox>().OnDie += OnPlayerDie;
            }
            players = players.OrderBy(x => Random.Range(0f, 100f)).ToList();
            Mode.SetupGame(players); 
        }

        if (isServerOnly)
        {
            return;
        }

        if (players.Any(p => p.transform == NetworkClient.localPlayer.transform))
        {
            _gameVCam.Priority = 20; 
        }
    }

    private void Update()
    {
        if (Mode != null)
        {
            Debug.Log($"Game Over? {Mode.CheckWinCondition(Players)}!");
        }
    }

    private void OnPlayerDie(Transform from)
    {
        PlayerCount--;
        Players.Remove(from);
    }

    protected static GameManager instance;

    /**
       Returns the instance of this singleton.
    */
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (GameManager)FindObjectOfType(typeof(GameManager));

                if (instance == null)
                {
                    Debug.LogError("An instance of " + typeof(GameManager) +
                       " is needed in the scene, but there is none.");
                }
            }

            return instance;
        }
    }
}

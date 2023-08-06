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
    [HideInInspector] public bool GameOver;
    /// <summary>
    /// Set true if the countdown has finished.
    /// </summary>
    [HideInInspector] public bool CountdownFinished { get; private set; }
    /// <summary>
    /// Set to true if the players have been moved to their position.
    /// </summary>
    [HideInInspector] public bool PlayersInPosition { get; private set; }
    [HideInInspector] public int CountdownTime { get; private set; }

    /// <summary>
    /// Called when the player either dies or finishes, and returns the place they finished in.
    /// </summary>
    public event System.Action<int> OnLocalPlayerFinish;

    public GameObject IdleBotPrefab;
    public List<Transform> Players;

    public Collider2D RespawnRoom;
    public CinemachineVirtualCamera GameVCam;

    [SerializeField] private string _defaultName;
    [SerializeField] private float _countdownLength;

    [SerializeField] private List<GameMode> _modes;
    [SerializeField] private GameObject _botPrefab;

    [SyncVar] private int _modeIndex;


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
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
            GameVCam.Priority = 20; 
        }

        CountdownFinished = false;
        PlayersInPosition = true;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float time = _countdownLength;
        while (time > 0)
        {
            yield return null;
            time -= Time.deltaTime;
            CountdownTime = Mathf.FloorToInt(time);
        }
        CountdownFinished = true;
    }

    private void Update()
    {
        if (CountdownFinished)
        {
            GameOver = Mode.CheckWinCondition(Players);
            if (GameOver)
            {
                foreach (var _ in from player in Players
                                  where player.GetComponent<NetworkBehaviour>().isLocalPlayer
                                  select new { })
                {
                    OnLocalPlayerFinish?.Invoke(1);
                }
                CountdownFinished = false;
                PlayersInPosition = false;

                Invoke(nameof(FinishGame), 3f);
            }
        }
    }

    private void FinishGame()
    {
        var playerListCopy = new List<Transform>(Players);
        foreach (var player in playerListCopy)
        {
            if (player.GetComponent<NetworkBehaviour>().isLocalPlayer)
            {
            }
            player.GetComponentInChildren<AgentHitbox>().Die();
        }
        CountdownFinished = false;
    }

    private void OnPlayerDie(Transform from)
    {
        if (!Mode.Teams && from.root.GetComponent<NetworkBehaviour>().isLocalPlayer)
        {
            OnLocalPlayerFinish(PlayerCount); 
        }
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

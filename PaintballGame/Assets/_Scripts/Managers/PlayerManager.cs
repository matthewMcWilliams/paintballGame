using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System;

public class PlayerManager : Singleton<PlayerManager>
{
    public List<Transform> Players { get; private set; }
    
    [SerializeField] private string _playerTag;

    private void Start()
    {
        Players = new List<Transform>();
        StartCoroutine(SetTransforms());
    }

    private IEnumerator SetTransforms()
    {
        Players.Clear();
        Players.AddRange(FindAllPlayers(_playerTag).Select(x => x.transform));
        yield return new WaitForEndOfFrame();
        yield return StartCoroutine(SetTransforms());
    }

    static List<GameObject> FindAllPlayers(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).ToList();
    }

    public Transform FindClosestPlayer(Transform from)
    {
        Transform closestPlayer = null;
        if (Players.Count == 0)
        {
            StartCoroutine(SetTransforms());
            if (Players.Count == 0)
            {
                //throw new Exception("NOT 2 MANY PLAYERS");
            }
        }
        float minimumDistance = Mathf.Infinity;

        foreach (var player in Players)
        {
            float distance = Vector3.Distance(from.position, player.position);
            if(distance != 0 && distance < minimumDistance && player.GetComponentInChildren<SpriteRenderer>().enabled)
            {
                closestPlayer = player;
                minimumDistance = distance;
            }
        }
        if (closestPlayer == null)
        {
            Debug.Log("No players in Range");
            return null;
        }

        return closestPlayer;
    }
}
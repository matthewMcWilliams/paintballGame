using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System;
using Unity.VisualScripting;

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
            if (distance != 0 && distance < minimumDistance && player.GetComponentInChildren<SpriteRenderer>().enabled)
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

    public List<Transform> GetOpponents(Transform from)
    {
        List<Transform> result = new List<Transform>();
        foreach (var player in Players)
        {
            float distance = Vector3.Distance(from.position, player.position);

            if (distance != 0 &&
                player.GetComponentInChildren<SpriteRenderer>().enabled &&
                !(
                    player.GetComponent<PlayerGameData>().TeamNumber == from.root.GetComponent<PlayerGameData>().TeamNumber &&
                    GameManager.Instance.Mode.Teams)
                 )
            {
                result.Add(player);
            }
        }
        return result;
    }

    public Transform FindClosestOpponent(Transform from)
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
            if(distance != 0 && 
                distance < minimumDistance && 
                player.GetComponentInChildren<SpriteRenderer>().enabled && 
                !(
                    player.GetComponent<PlayerGameData>().TeamNumber == from.root.GetComponent<PlayerGameData>().TeamNumber && 
                    GameManager.Instance.Mode.Teams)
                 )
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
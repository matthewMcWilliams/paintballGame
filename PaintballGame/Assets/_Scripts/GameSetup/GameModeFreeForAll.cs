using Mirror.Examples.MultipleAdditiveScenes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "_FreeForAll", menuName = "Game Modes/Free For All")]
public class GameModeFreeForAll : GameMode
{
    [SerializeField] private float spawnPadding = 5f;

    public override string Text => $"{GameManager.Instance.PlayerCount} / {MinPlayers}";

    public override void SetupGame(List<Transform> players)
    {

        SelectPositions(players);
    }

    public override bool CheckWinCondition(List<Transform> players) => players.Count <= 1;

    private void SelectPositions(List<Transform> players)
    {
        BoundsInt bounds = Generator.Instance.Bounds;

        Bounds q1 = new Bounds { min = bounds.min, max = bounds.center };
        Bounds q2 = new Bounds { min = bounds.center, max = bounds.max };
        Bounds q3 = new Bounds { min = new Vector3(bounds.min.x, bounds.center.y), max = new Vector3(bounds.center.x, bounds.max.y) };
        Bounds q4 = new Bounds { min = new(bounds.center.x, bounds.min.y), max = new Vector3(bounds.max.x, bounds.center.y) };

        Bounds[] quarterList = new Bounds[] { q1, q2, q3, q4 };
        quarterList.OrderBy(x => Random.Range(0f, 1f));

        Queue<Bounds> spawnQueue = new(quarterList);

        int counter = 0;
        foreach (var p in players)
        {
            Bounds selectedBounds = spawnQueue.Dequeue();

            Vector2 newPosition = new Vector2 ( Random.Range(selectedBounds.min.x, selectedBounds.max.x), 
                Random.Range(selectedBounds.min.y, selectedBounds.max.y) );

            p.GetComponent<PlayerGameData>().TeamNumber = counter;
            p.GetComponentInChildren<AgentRenderer>().TeamColor = Color.HSVToRGB(RandomValue(counter), 0.71f, 0.71f);

            p.position = newPosition;

            if (spawnQueue.Count == 0)
            {
                spawnQueue = new(quarterList);
            }

            counter++;
        }

    }

    public static float RandomValue(int state)
    {
        Random.InitState(state);
        return Random.value;
    }
}

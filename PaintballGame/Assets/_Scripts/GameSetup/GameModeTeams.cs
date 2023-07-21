using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "_Teams", menuName = "Game Modes/Teams")]
public class GameModeTeams : GameMode
{
    [Range(0, 4)] public int NumberOfTeams = 2;

    public override string Text
    {
        get
        {
            int[] teamPlayerCount = new int[NumberOfTeams];
            foreach (var player in GameManager.Instance.Players)
            {
                teamPlayerCount[player.GetComponent<PlayerGameData>().TeamNumber]++;
            }
            return string.Join(" - ", teamPlayerCount);
        }
    }

    public override void SetupGame(List<Transform> players)
    {
        BoundsInt bounds = Generator.Instance.Bounds;

        Bounds q1 = new Bounds { min = bounds.min, max = bounds.center };
        Bounds q2 = new Bounds { min = bounds.center, max = bounds.max };
        Bounds q3 = new Bounds { min = new Vector3(bounds.min.x, bounds.center.y), max = new Vector3(bounds.center.x, bounds.max.y) };
        Bounds q4 = new Bounds { min = new(bounds.center.x, bounds.min.y), max = new Vector3(bounds.max.x, bounds.center.y) };

        Bounds[] quarterList = new Bounds[] { q1, q2, q3, q4 };
        quarterList.OrderBy(x => Random.Range(0f, 1f));

        Bounds[] teamQuarters = new Bounds[NumberOfTeams];
        for (int i = 0; i < NumberOfTeams; i++)
        {
            teamQuarters[i] = quarterList[i];

        }
        Debug.Log(string.Join(", ", teamQuarters));


        int teamIndex = 0;

        foreach (var p in players)
        {
            p.GetComponent<PlayerGameData>().TeamNumber = teamIndex;
            p.GetComponentInChildren<AgentRenderer>().TeamColor = Color.HSVToRGB(RandomValue(teamIndex), 0.71f, 0.71f);


            Bounds selectedBounds = teamQuarters[teamIndex];

            Vector2 newPosition = new Vector2(Random.Range(selectedBounds.min.x, selectedBounds.max.x),
                Random.Range(selectedBounds.min.y, selectedBounds.max.y));

            p.position = newPosition;

            teamIndex++;
            teamIndex %= NumberOfTeams;
        }
    }

    public override bool CheckWinCondition(List<Transform> players)
    {
        int teamIndex = players[0].GetComponent<PlayerGameData>().TeamNumber;
        foreach (var player in players)
        {
            if (teamIndex != player.GetComponent<PlayerGameData>().TeamNumber)
            {
                return false;
            }
        }
        return true;
    }

    private static void SelectQuarters(Bounds[] quarterList, ref Bounds[] teamQuarters, int i)
    {
        int rndNumber = Random.Range(0, quarterList.Length);
        if (teamQuarters.Any(q => q == quarterList[rndNumber]))
        {
            teamQuarters[i] = quarterList[rndNumber];
            SelectQuarters(quarterList, ref teamQuarters, i);
        }
    }

    static float RandomValue(int state)
    {
        Random.InitState(state);
        return Random.value;
    }
}

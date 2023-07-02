using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class GenerationUtility
{
    internal static List<Vector2> GenerateField(BoundsInt bounds, float distance, int checkAmt)
    {
        Queue<Vector2> checkQueue = new Queue<Vector2>();
        List<Vector2> list = new List<Vector2>();

        Vector2 currentPoint = new Vector2 {
            x = Random.Range(bounds.xMin, bounds.xMax) ,
            y = Random.Range(bounds.yMin, bounds.yMax)
        };

        checkQueue.Enqueue(currentPoint);
        list.Add(currentPoint);
        while(checkQueue.Count > 0)
        {
            currentPoint = checkQueue.Dequeue();
            for (int i = 0; i < checkAmt; i++)
            {
                Vector2 dir = Random.insideUnitCircle.normalized * distance;
                if (!Inside(bounds, currentPoint + dir) || list.Any(p => Vector2.SqrMagnitude(p - (currentPoint + dir)) < distance * distance))
                {
                    Debug.Log(currentPoint + dir);
                    continue; 
                }
                checkQueue.Enqueue(currentPoint+dir);
                list.Add(currentPoint+dir);
            }
        }

        return list;
    }

    private static bool Inside(BoundsInt bounds, Vector2 pos)
    {
        return (bounds.xMin < pos.x) && (bounds.yMin < pos.y) && (bounds.xMax > pos.x) && (bounds.yMax > pos.y);
    }

    public static HashSet<Vector2Int> GenerateIsland(Vector2Int startPos, IslandDataSO islandData)
    {
        HashSet<Vector2Int> result = new HashSet<Vector2Int>();
        for (int i = 0; i < islandData.numberOfLegs; i++)
        {
            result.UnionWith(GenerateLeg(startPos, islandData.legLength));
        }
        return result;
    }

    private static HashSet<Vector2Int> GenerateLeg(Vector2Int startPos, int legLength)
    {
        HashSet<Vector2Int> result = new();
        Vector2Int currentPos = startPos;
        result.Add(currentPos);
        for (int i = 0; i < legLength; i++)
        {
            currentPos += directions[Random.Range(0, directions.Count)];
            result.Add(currentPos);
        }
        return result;
    }

    static List<Vector2Int> directions = new List<Vector2Int>
    {
        new Vector2Int(1,0),
        new Vector2Int(-1,0),
        new Vector2Int(0,1),
        new Vector2Int(0,-1)
    };
}
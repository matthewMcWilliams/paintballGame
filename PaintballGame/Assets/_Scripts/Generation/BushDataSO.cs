using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "_BushData", menuName = "Generation/Bush")]

public class BushDataSO : Randomizable
{
    private HashSet<Vector2Int> _bushPositions = new();
    [SerializeField] private float _clumpDistance = 10f;
    [SerializeField] private int _checkAmt = 10;
    [SerializeField] private float _clumpProbability = 0.4f;
    [SerializeField] private IslandDataSO _islandData;

    public override void Randomize()
    {
        _bushPositions.Clear();

        List<Vector2Int> roundedPositions = GenerateField();

        HashSet<Vector2Int> positions = RandomlyMakeIslands( roundedPositions );

        _bushPositions.UnionWith(positions);
    }

    private HashSet<Vector2Int> RandomlyMakeIslands( List<Vector2Int> roundedPositions)
    {
        HashSet<Vector2Int> positions = new();
        foreach (Vector2Int clumpLocation in roundedPositions)
        {
            if (Random.Range(0f,1f) < _clumpProbability)
            {
                positions.UnionWith(GenerationUtility.GenerateIsland(clumpLocation, _islandData));
            }
        }
        return positions;
    }

    private List<Vector2Int> GenerateField()
    {
        List<Vector2> positions = GenerationUtility.GenerateField(Bounds, _clumpDistance, _checkAmt);
        Debug.Log(positions.Count);
        List<Vector2Int> roundedPositions = positions.Select(p => Vector2Int.RoundToInt(p)).ToList();
        return roundedPositions;
    }

    public bool IncludesPos(Vector2Int position)
    {
        return _bushPositions.Contains(position);
    }
}

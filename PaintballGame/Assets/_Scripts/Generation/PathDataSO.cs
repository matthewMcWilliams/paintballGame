using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName ="_PathData", menuName ="Generation/Path")]
public class PathDataSO : Randomizable
{
    [SerializeField] private BoundsInt _bounds;
    [SerializeField] private int _xMargin, _yMargin;
    [SerializeField] private int _xPadding, _yPadding;
    [SerializeField] private int _thickness = 1;
    [SerializeField] private bool _makeStartTrail, _makeEndTrail;
    [SerializeField] private Vector2Int _startPos, _endPos;

    enum Quadrant
    {
        Q1, Q2, Q3, Q4
    }

    private HashSet<Vector2Int> _pathPositions = new();

    public override void Randomize()
    {
        _pathPositions.Clear();

        Quadrant quadrant1 = (Quadrant)Random.Range(0, 4);
        Quadrant quadrant2 = (Quadrant)((int)(quadrant1 + 2) % 4);

        // Point 1
        Vector2Int pos = GenerateRandomPoint(quadrant1);
        Vector2Int pos2 = GenerateRandomPoint(quadrant2);

        if (_makeStartTrail)
        {
            MakePath(_startPos, pos); 
        }
        MakePath(pos, pos2);
        if (_makeEndTrail)
        {
            MakePath(pos2, _endPos); 
        }
    }

    private void MakePath(Vector2Int pos, Vector2Int pos2)
    {
        MakePoint(pos);
        for (int x = Mathf.Min(pos.x, pos2.x); x < Mathf.Max(pos.x,pos2.x); x++)
        {
            for (int i = -_thickness; i < _thickness; i++)
            {
                _pathPositions.Add(new(x, pos.y + i));

            }
        }
        MakePoint(new(pos2.x, pos.y));
        for (int y = Mathf.Min(pos.y, pos2.y); y < Mathf.Max(pos.y,pos2.y); y++)
        {
            for (int i = -_thickness; i < _thickness; i++)
            {
                _pathPositions.Add(new(pos2.x + i, y));
            }
        }
        MakePoint(pos2);
    }

    private void MakePoint(Vector2Int pos)
    {
        for (int x = -_thickness; x < _thickness; x++)
        {
            for (int y = -_thickness; y < _thickness; y++)
            {
                var p = pos + new Vector2Int(x, y);
                _pathPositions.Add(p);
            }
        }
    }

    private Vector2Int GenerateRandomPoint(Quadrant quadrant)
    {
        Vector2Int pos = new()
        {
            x = Random.Range(_xMargin, _xMargin + _xPadding),
            y = Random.Range(_yMargin, _yMargin + _yPadding)
        };
        pos = GetPos(quadrant, pos);
        return pos;
    }

    internal bool Includes(int x, int y) => _pathPositions.Contains(new(x, y));

    private Vector2Int GetPos(Quadrant quadrant, Vector2Int pos)
    {
        return quadrant switch
        {
            Quadrant.Q1 => (Vector2Int)_bounds.max - pos,
            Quadrant.Q2 => new Vector2Int { x=_bounds.xMin+pos.x, y = _bounds.yMax -pos.y },
            Quadrant.Q3 => (Vector2Int)_bounds.min + pos,
            Quadrant.Q4 => new Vector2Int { x = _bounds.xMax - pos.x, y = _bounds.yMin + pos.y },
            _ => Vector2Int.zero
        };
    }
}

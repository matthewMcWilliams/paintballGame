using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathHandler : TileHandler
{
    [SerializeField] private PathDataSO _pathData;
    [SerializeField] private TileBase _pathTile;

    protected override bool TryHandling(int x, int y, Vector2Int mapSeedOffset)
    {
        if (_pathData.Includes(x,y))
        {
            TileSetCreator.Instance.SetTile(new(x, y), _pathTile);
            return true;
        }
        return false;
    }

}

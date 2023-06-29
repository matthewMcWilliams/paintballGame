using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassHandler : TileHandler
{
    [SerializeField] private TileBase _grassTile;

    protected override bool TryHandling(int x, int y, Vector2Int mapSeedOffset)
    {
        TileSetCreator.Instance.SetTile(new Vector2 { x = x, y = y }, _grassTile);
        return true;
    }
}

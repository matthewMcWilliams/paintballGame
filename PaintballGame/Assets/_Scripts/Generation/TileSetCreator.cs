using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSetCreator : Singleton<TileSetCreator>
{
    [SerializeField] private Tilemap _groundTilemap;

    public void SetTile(Vector2 worldPos, TileBase tile)
    {
        _groundTilemap.SetTile(_groundTilemap.WorldToCell(worldPos), tile);
    }

    public void Clear()
    {
        _groundTilemap.ClearAllTiles();
    }
}

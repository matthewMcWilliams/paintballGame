using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSetCreator : Singleton<TileSetCreator>
{
    [SerializeField] private Tilemap _groundTilemap;
    [SerializeField] private Tilemap _bushTilemap;
    [SerializeField] private Tilemap _treeTilemap;

    public void SetTile(Vector2 worldPos, TileTypeSO tileData)
    {
        _groundTilemap.SetTile(_groundTilemap.WorldToCell(worldPos), tileData.Tile);
        if (tileData.IsBush)
        {
            _bushTilemap.SetTile(_bushTilemap.WorldToCell(worldPos), tileData.Tile);
        }
        if (tileData.IsTree)
        {
            _treeTilemap.SetTile(_treeTilemap.WorldToCell(worldPos), tileData.Tile);
        }
    }

    public void Clear()
    {
        _groundTilemap.ClearAllTiles();
        _bushTilemap.ClearAllTiles();
        _treeTilemap.ClearAllTiles();
    }

    public int NeighborCount(TileTypeSO pathTile, int x, int y)
    {
        int counter = 0;
        counter += (_groundTilemap.GetTile(new(x-1, y-1)) == pathTile.Tile) ? 1 : 0;
        counter += (_groundTilemap.GetTile(new(x, y-1)) == pathTile.Tile) ? 1 : 0;
        counter += (_groundTilemap.GetTile(new(x+1, y-1)) == pathTile.Tile) ? 1 : 0;
        counter += (_groundTilemap.GetTile(new(x-1, y)) == pathTile.Tile) ? 1 : 0;
        counter += (_groundTilemap.GetTile(new(x+1, y)) == pathTile.Tile) ? 1 : 0;
        counter += (_groundTilemap.GetTile(new(x-1, y+1)) == pathTile.Tile) ? 1 : 0;
        counter += (_groundTilemap.GetTile(new(x, y+1)) == pathTile.Tile) ? 1 : 0;
        counter += (_groundTilemap.GetTile(new(x+1, y+1)) == pathTile.Tile) ? 1 : 0;
        return counter;
    }
}

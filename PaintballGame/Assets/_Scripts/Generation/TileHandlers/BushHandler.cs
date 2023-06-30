using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BushHandler : TileHandler
{
    [SerializeField] private TileTypeSO _bushTile;
    [SerializeField] private PathDataSO _pathData;
    [SerializeField] private BushDataSO _bushData;
    [SerializeField] private int _probabilityMax, _probabilityMin;
    [SerializeField] private int _bushMult, _pathMult;

    protected override bool TryHandling(int x, int y, Vector2Int mapSeedOffset)
    {
        int numberOfPathTiles = _pathData.NeighborCount(x, y);
        int numberOfBushTiles = TileSetCreator.Instance.NeighborCount(_bushTile, x, y);
        int probability = numberOfBushTiles * _bushMult + numberOfPathTiles * _pathMult;
        
        if (!_bushData.IncludesPos(new(x,y)) && probability < Random.Range(_probabilityMin, _probabilityMax + 1))
            return false;
        TileSetCreator.Instance.SetTile(new(x, y), _bushTile);
        return true;
        
    }

}

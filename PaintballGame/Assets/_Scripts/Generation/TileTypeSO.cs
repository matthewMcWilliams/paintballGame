using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "_TileType", menuName = "Generation/TileType")]
public class TileTypeSO : ScriptableObject
{
    public TileBase Tile;
    public bool GenerateCollison = false;
}
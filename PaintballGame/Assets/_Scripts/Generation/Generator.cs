using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : Singleton<Generator>
{
    public BoundsInt Bounds;

    [SerializeField] private TileHandler _firstHandler;
    [SerializeField] private List<Randomizable> _randomizables;
    [SerializeField] private bool _randomize;


    public void Generate()
    {
        Clear();
        if (_randomize)
        {
            foreach (var rand in _randomizables)
            {
                rand.Randomize();
                rand.Bounds = Bounds;
            }
        }

        for (int x = Bounds.xMin; x < Bounds.xMax; x++)
        {
            for (int y = Bounds.yMin; y < Bounds.yMax; y++)
            {
                _firstHandler.Handle(x, y,Vector2Int.zero);
            }
        }
    }

    public void Clear() => TileSetCreator.Instance.Clear();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Bounds.center, Bounds.size);
    }
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private BoundsInt _bounds;
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
                rand.Bounds = _bounds;
            }
        }

        for (int x = _bounds.xMin; x < _bounds.xMax; x++)
        {
            for (int y = _bounds.yMin; y < _bounds.yMax; y++)
            {
                _firstHandler.Handle(x, y,Vector2Int.zero);
            }
        }
    }

    public void Clear() => TileSetCreator.Instance.Clear();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }
}

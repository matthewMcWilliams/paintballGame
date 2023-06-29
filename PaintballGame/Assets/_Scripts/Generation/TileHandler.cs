using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileHandler : MonoBehaviour
{
    [SerializeField]
    private TileHandler Next;

    public bool Handle(int x, int y, Vector2Int mapSeedOffset)
    {
        if (TryHandling(x, y, mapSeedOffset))
            return true;
        if (Next != null)
            return Next.Handle(x, y, mapSeedOffset);
        return false;
    }

    protected abstract bool TryHandling(int x, int y, Vector2Int mapSeedOffset);
}
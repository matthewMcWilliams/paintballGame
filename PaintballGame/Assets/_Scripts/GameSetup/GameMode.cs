using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : ScriptableObject
{
    public int MinPlayers, maxPlayers;
    public bool Teams = false;
    public abstract string Text { get; }
    public abstract void SetupGame(List<Transform> players);
    public abstract bool CheckWinCondition(List<Transform> players);
}

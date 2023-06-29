using UnityEngine;

[CreateAssetMenu(fileName = "_PathData", menuName = "Generation/Path")]
public abstract class Randomizable : ScriptableObject
{
    public abstract void Randomize();
}
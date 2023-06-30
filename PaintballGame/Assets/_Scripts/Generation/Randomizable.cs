using UnityEngine;

[CreateAssetMenu(fileName = "_PathData", menuName = "Generation/Path")]
public abstract class Randomizable : ScriptableObject
{
    public BoundsInt Bounds;
    public abstract void Randomize();
}
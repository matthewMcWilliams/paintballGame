using UnityEngine;

[CreateAssetMenu(fileName = "_IslandData", menuName = "Generation/IslandData")]
public class IslandDataSO : ScriptableObject
{
    public int numberOfLegs;
    public int legLength;
}
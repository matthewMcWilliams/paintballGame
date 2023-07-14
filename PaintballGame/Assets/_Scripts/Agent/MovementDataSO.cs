using UnityEngine;

[CreateAssetMenu(fileName = "_MovmentData", menuName ="Agent/Movment Data")]
public class MovementDataSO : ScriptableObject
{
    [Range(0, 15)] public float MaxSpeed, SlowSpeedBush, SlowSpeedTree;
    [Range(0, 40)] public float MaxAcceleration;
    public LayerMask BushLayerMask, TreeLayerMask;
}

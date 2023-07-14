using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_ArmorData", menuName = "Weapons/Armor")]
public class ArmorDataSO : ScriptableObject
{
    [Range(0f, 1f)] public float BounceChance = 0.8f;
    [Range(0.1f, 1f), Tooltip("A greater factor means faster camoflauge")] public float CamoFactor = 0.3f;
}
